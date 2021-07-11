using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InputManager : MonoBehaviour
{
	[Header("References")]
	public SettingsInjecter SettingsInjecter;
	public PlayerSelections PlayerSelections;
	public LocationMarkers LocationMarkers;
	public EventSystem EventSystem;

	[Header("UI References")]
	public Canvas UIcanvas;
	public Tooltip Tooltip;
	public Menu Menu;
	public BuildMenu BuildMenu;
	public RectTransform SelectionBox;
	public BuildGhost BuildGhost;

	[Header("References")]
	public Transform HeroContainer;

	[Header("Camera")]
	public CameraController CameraController;

	[Header("Map")]
	public MapGenerator MapGenerator;
	public Tilemap Tilemap;

	[Header("Debug")]
	public GameObject Highlight;

	private InputAction _playerMovement;
	private InputAction _playerLeftClick;
	private InputAction _playerLeftClickHold;
	private InputAction _playerRightClick;
	private InputAction _toggleMenu;
	private InputAction _toggleBuildMenu;
	private InputAction _nextHero;
	private InputAction _cameraZoom;
	private InputAction _cameraBoost;
	private InputAction _locationSave;
	private InputAction _locationSnap;

	private Vector2 pointLastClicked;
	private GameObject mouseMarker;
	private bool holdingMouseDown;
	private float ppu;

	private GameObject rectMarker1;
	private GameObject rectMarker2;

	private void Awake()
	{
		PlayerControls _playerControls = new PlayerControls();
		_playerControls.Enable();

		_playerMovement = _playerControls.Player.Movement;

		_playerLeftClick = _playerControls.Player.LeftClick;
		_playerLeftClick.performed += ctx => OnLeftClick(Mouse.current.position.ReadValue(), ctx);

		_playerLeftClickHold = _playerControls.Player.LeftClickHold;
		_playerLeftClickHold.performed += ctx => OnLeftClickHold(Mouse.current.position.ReadValue(), ctx);

		_playerRightClick = _playerControls.Player.RightClick;
		_playerRightClick.performed += ctx => OnRightClick(Mouse.current.position.ReadValue(), ctx);

		_cameraZoom = _playerControls.Camera.Zoom;
		_cameraZoom.performed += ctx => OnCameraZoom(ctx);

		_cameraBoost = _playerControls.Camera.Boost;
		_cameraBoost.started += ctx => OnCameraBoostDown(ctx);
		_cameraBoost.canceled += ctx => OnCameraBoostUp(ctx);

		_toggleMenu = _playerControls.HotKeys.ToggleMenu;
		_toggleMenu.performed += ctx => OnToggleMenu();

		_toggleBuildMenu = _playerControls.HotKeys.ToggleBuildMenu;
		_toggleBuildMenu.performed += ctx => OnToggleBuildMenu();

		_locationSave = _playerControls.HotKeys.SaveLocationMarker;
		_locationSave.performed += ctx => OnLocationSave((int) ctx.ReadValue<float>());

		_locationSnap = _playerControls.HotKeys.SnapToLocationMarker;
		_locationSnap.performed += ctx => OnLocationSnap((int) ctx.ReadValue<float>());

		_nextHero = _playerControls.HotKeys.NextHero;
		_nextHero.performed += ctx => OnNextHero();

		ppu = UIcanvas.GetComponent<CanvasScaler>().referencePixelsPerUnit;
	}

	private void Update() 
	{
		Vector3 mpos = Mouse.current.position.ReadValue();

		if (SettingsInjecter.GameSettings.IsBuilding)
		{
			BuildGhost.UpdateLocation(mpos);
		}
		else
		{
			PlayerSelections.Hover(GetSelectableUnderCursor(mpos));	
		}

		if (holdingMouseDown == true) 
		{ 
			if (!Mouse.current.leftButton.isPressed) { OnHoldRelease(mpos); }
			UpdateSelectionBoxSize(mpos); 
		}
	}

	private void FixedUpdate() 
	{
		CameraController.Move(_playerMovement.ReadValue<Vector2>());
	}

	// PLAYER

	private void OnNextHero()
	{
		if (HeroContainer.childCount == 0) { return; }

		GameObject toSelect = null;

		if (PlayerSelections.SelectedObjects.Count >= 1)
		{
			GameObject currentSelected = PlayerSelections.SelectedObjects[0];
			for (int i = 0; i < HeroContainer.childCount; i++)
			{
				if (HeroContainer.GetChild(i).gameObject == currentSelected)
				{
					// if more, get next
					if (HeroContainer.childCount >= 2)
					{
						toSelect = HeroContainer.GetChild((i + 1) % HeroContainer.childCount).gameObject;
						break;
					}
					// if not more, return
					else
					{
						return;
					}
				}
			}
		}
		else
		{
			toSelect = HeroContainer.GetChild(0).gameObject;
		}

		PlayerSelections.DeselectAll();
		PlayerSelections.Select(toSelect);
		CameraController.SetCameraFollow(toSelect);
	}

	// CAMERA
	private void OnCameraZoom(InputAction.CallbackContext ctx)
	{
		CameraController.Zoom(ctx.ReadValue<Vector2>().y);
	}

	private void OnCameraBoostDown(InputAction.CallbackContext ctx)
	{
		CameraController.Boost = true;
	}

	private void OnCameraBoostUp(InputAction.CallbackContext ctx)
	{
		CameraController.Boost = false;
	}

	private void OnLocationSave(int index)
	{
		LocationMarkers.Locations[index] = CameraController.transform.position;
	}

	private void OnLocationSnap(int index)
	{
		print(LocationMarkers.Locations[index]);
		Vector3 loc = LocationMarkers.Locations[index];
		if (loc != null)
		{
			CameraController.MoveTo(LocationMarkers.Locations[index]);
		}
	}
	
	// CLICK
	private void OnLeftClick(Vector3 mpos, InputAction.CallbackContext context)
	{
		if (SettingsInjecter.GameSettings.IsBuilding)
		{
			SettingsInjecter.GameSettings.IsBuilding = false;
			BuildGhost.Disable();
			BuildGhost.Build(mpos);
		}
		else
		{
			pointLastClicked = mpos;
			if (!ClickedUI(mpos))
			{
				GameObject obj = GetSelectableUnderCursor(mpos);
				if (obj != null) 
				{
					PlayerSelections.Select(GetSelectableUnderCursor(mpos)); 
				}
				else
				{
					PlayerSelections.DeselectAll();
					TurnOffMenus();
				}
			}
		}
	}

	private void OnRightClick(Vector3 mpos, InputAction.CallbackContext context)
	{
		PlayerSelections.DeselectAll();
		SelectionBox.gameObject.SetActive(false);

		if (SettingsInjecter.GameSettings.IsBuilding)
		{
			BuildGhost.Disable();
		}

		if (SettingsInjecter.GameSettings.MenuIsOpen || SettingsInjecter.GameSettings.BuildMenuIsOpen)
		{
			Menu.TweenOutMenu();
			BuildMenu.TweenOutMenu();
		}

		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Vector3 worldPoint = ray.GetPoint(0);
		Vector2Int tileLoc = TileConversion.WorldToTile(worldPoint);

		// PRINT TRAVEL TYPES
		// foreach (TileTravelType type in SettingsInjecter.MapSettings.GetTile(tileLoc).TravelType)
		// {
		// 	print(type);
		// }

		// PRINT ITEMS IN CONTAINER
		foreach (GameObject obj in SettingsInjecter.MapSettings.GetTile(tileLoc).ContainedObjects)
		{
			Container container = obj.GetComponent<Container>();
			if (container != null)
			{
				foreach (var item in container.items)
				{
					print(SettingsInjecter.ItemTable.GetById(item.Key) + ": " + item.Value);
				}
			}
		}
	}

	private GameObject GetSelectableUnderCursor(Vector3 mpos)
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, SettingsInjecter.GameSettings.SelectableLayers);

		if (hit) 
		{
			return hit.transform.gameObject;
		}
		else
		{
			return null;
		}
	}

	// HOLD
	private void OnLeftClickHold(Vector3 mpos, InputAction.CallbackContext context)
	{
		holdingMouseDown = true;
		SelectionBox.gameObject.SetActive(true);
	}

	private void OnHoldRelease(Vector2 currPos)
	{
		SelectionBox.gameObject.SetActive(false);
		holdingMouseDown = false;

		Vector2 uiRectCentre = currPos - pointLastClicked;
		Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(pointLastClicked.x + (uiRectCentre.x / 2), pointLastClicked.y + (uiRectCentre.y / 2), 1));

		Vector2 botLeft = Camera.main.ScreenToWorldPoint(pointLastClicked);
		Vector2 topRight = Camera.main.ScreenToWorldPoint(currPos);
		Vector2 size = (topRight - botLeft) / 2;

		List<GameObject> playerUnits = new List<GameObject>();
		List<GameObject> allSelectables = new List<GameObject>();

		RaycastHit2D[] hits = Physics2D.BoxCastAll(worldPos, size, 0, Vector2.zero, Mathf.Infinity, SettingsInjecter.GameSettings.SelectableLayers);
		foreach (RaycastHit2D hit in hits)
		{
			GameObject obj = hit.transform.gameObject;

			if (obj.GetComponent<NPCBase>() != null)
				playerUnits.Add(obj);

			allSelectables.Add(obj);
		}

		if (playerUnits.Count > 0)
		{
			PlayerSelections.Select(playerUnits);
		}
		else if (allSelectables.Count > 0 )
		{
			PlayerSelections.Select(allSelectables[0]);
		}
	}

	private void UpdateSelectionBoxSize(Vector2 currPos)
	{
		float width = currPos.x - pointLastClicked.x;
		float height = currPos.y - pointLastClicked.y;

		SelectionBox.sizeDelta = new Vector2(width, height);
		SelectionBox.position = pointLastClicked;
	}

	// UTILS

	private bool ClickedUI(Vector3 mpos)
	{ 
		PointerEventData PointerEventData = new PointerEventData(EventSystem);
		PointerEventData.position = mpos;

		List<RaycastResult> hits = new List<RaycastResult>();
		EventSystem.current.RaycastAll(PointerEventData, hits);

		if(hits.Count > 0)
		{
    		foreach(var result in hits)
    		{
				if (result.gameObject.layer == Layer.UI.GetHashCode())
				{
					return true;
				}
    		}
		}
		return false;
	}

	// private void UpdateTileCursor()
	// {
	// 	Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
	// 	Vector3 worldPoint = ray.GetPoint(0);
	// 	Vector2Int mposInWorld = TileConversion.WorldToTile(worldPoint);

	// 	if (mposInWorld.x < 0 || mposInWorld.x >= SettingsInjecter.MapSettings.MapSize || mposInWorld.y < 0 || mposInWorld.y >= SettingsInjecter.MapSettings.MapSize)  
	// 	{ 
	// 		tileCursor.SetActive(false);
	// 		return; 
	// 	}

	// 	tileCursor.transform.position = TileConversion.TileToWorld3D(mposInWorld);
	// 	tileCursor.SetActive(true);
	// }

	// MENU
	private void OnToggleMenu()
	{
		Menu.ToggleMenu();
	}

	private void OnToggleBuildMenu()
	{
		BuildMenu.ToggleBuildMenu();
	}

	private void TurnOffMenus()
	{
		if (SettingsInjecter.GameSettings.MenuIsOpen) { Menu.TweenOutMenu(); }
		if (SettingsInjecter.GameSettings.BuildMenuIsOpen) { BuildMenu.TweenOutMenu(); }
	}

}
