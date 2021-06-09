using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	public MapSettings MapSettings;
	public GameObject Marker;

	[SerializeField] private CameraController cameraController;
	[SerializeField] private Canvas UIcanvas;
	[SerializeField] private EventSystem eventSystem;
	[SerializeField] private MapGenerator mapGenerator;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private Tooltip tooltip;
	[SerializeField] private Menu menu;
	[SerializeField] private RectTransform selectionBox;
	[SerializeField] private GameObject tileCursor;
	[SerializeField] private LayerMask selectable;

	private InputAction _playerMovement;
	private InputAction _playerLeftClick;
	private InputAction _playerLeftClickHold;
	private InputAction _playerRightClick;
	private InputAction _toggleMenu;
	private InputAction _cameraZoom;

	private Vector2 pointLastClicked;
	private bool holdingMouseDown;
	private float ppu;

	private void OnEnable()
	{
		_playerMovement.Enable();
		_playerLeftClick.Enable();
		_playerLeftClickHold.Enable();
		_playerRightClick.Enable();
		_toggleMenu.Enable();
		_cameraZoom.Enable();
	}

	private void Awake()
	{
		PlayerControls _playerControls = new PlayerControls();

		_playerMovement = _playerControls.Player.Movement;

		_playerLeftClick = _playerControls.Player.LeftClick;
		_playerLeftClick.performed += ctx => OnLeftClick(Mouse.current.position.ReadValue(), ctx);

		_playerLeftClickHold = _playerControls.Player.LeftClickHold;
		_playerLeftClickHold.performed += ctx => OnLeftClickHold(Mouse.current.position.ReadValue(), ctx);

		_playerRightClick = _playerControls.Player.RightClick;
		_playerRightClick.performed += ctx => OnRightClick(Mouse.current.position.ReadValue(), ctx);

		_toggleMenu = _playerControls.Player.ToggleMenu;
		_toggleMenu.performed += ctx => OnToggleMenu();

		_cameraZoom = _playerControls.Camera.Zoom;
		_cameraZoom.performed += OnCameraZoom;

		ppu = UIcanvas.GetComponent<CanvasScaler>().referencePixelsPerUnit;
	}

	private void Start() 
	{
		selectionBox.gameObject.SetActive(false);
		tileCursor.SetActive(false);
	}

	private void Update() 
	{
		Vector3 mpos = Mouse.current.position.ReadValue();

		tooltip.Hover(GetSelectableUnderCursor(mpos));	

		if (holdingMouseDown == true) 
		{ 
			if (!Mouse.current.leftButton.isPressed) { OnHoldRelease(); }
			UpdateSelectionBoxSize(mpos); 
		}
	}

	private void FixedUpdate() 
	{
		Vector2 moveDir = _playerMovement.ReadValue<Vector2>();
		if (moveDir == Vector2.zero) { return; }
		cameraController.Move(moveDir);
	}

	private void OnCameraZoom(InputAction.CallbackContext context)
	{
		cameraController.Zoom(context.ReadValue<Vector2>().y);
	}

	// CLICK
	private void OnLeftClick(Vector3 mpos, InputAction.CallbackContext context)
	{
		pointLastClicked = mpos;
		if (!ClickedUI(mpos))
		{
			GameObject obj = GetSelectableUnderCursor(mpos);
			if (obj != null) 
			{
				tooltip.Select(GetSelectableUnderCursor(mpos)); 
			}
			else
			{
				tooltip.DeselectAll();
			}
		}
	}

	private void OnRightClick(Vector3 mpos, InputAction.CallbackContext context)
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Vector3 worldPoint = ray.GetPoint(0);
		Vector2Int tileLoc = TileConversion.WorldToTile(worldPoint);

		// tooltip.DeselectAll();
		selectionBox.gameObject.SetActive(false);

		print(tilemap.GetTile(new Vector3Int(tileLoc.x, tileLoc.y, 0)));
	}

	private GameObject GetSelectableUnderCursor(Vector3 mpos)
	{
		Ray ray = Camera.main.ScreenPointToRay(mpos);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100f, selectable);

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
		selectionBox.transform.position = pointLastClicked;
		selectionBox.gameObject.SetActive(true);
	}

	private void OnHoldRelease()
	{
		selectionBox.gameObject.SetActive(false);
		holdingMouseDown = false;

		Vector3 worldPos = Camera.main.ScreenToWorldPoint(selectionBox.anchoredPosition);
		Vector2 size = selectionBox.sizeDelta / 64;
		
		// Instantiate(Marker, new Vector3(worldPos.x, worldPos.y, 1), Quaternion.identity);

		List<GameObject> playerUnits = new List<GameObject>();
		List<GameObject> allSelectables = new List<GameObject>();

		RaycastHit2D[] hits = Physics2D.BoxCastAll(worldPos, size, 0, Vector2.zero, Mathf.Infinity, selectable);
		foreach (RaycastHit2D hit in hits)
		{
			GameObject obj = hit.transform.gameObject;

			if (obj.GetComponent<NPCBase>() != null)
				playerUnits.Add(obj);

			allSelectables.Add(obj);
		}

		if (playerUnits.Count > 0)
		{
			tooltip.Select(playerUnits);
		}
		else if (allSelectables.Count > 0 )
		{
			tooltip.Select(allSelectables[0]);
		}
	}

	private void UpdateSelectionBoxSize(Vector2 currPos)
	{
		float width = currPos.x - pointLastClicked.x;
		float height = currPos.y - pointLastClicked.y;

		selectionBox.sizeDelta = new Vector2(width, height);
		selectionBox.anchoredPosition = pointLastClicked + new Vector2(width/2, height/2);
	}

	// UTILS

	private bool ClickedUI(Vector3 mpos)
	{ 
		PointerEventData PointerEventData = new PointerEventData(eventSystem);
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

	private void UpdateTileCursor()
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Vector3 worldPoint = ray.GetPoint(0);
		Vector2Int mposInWorld = TileConversion.WorldToTile(worldPoint);

		if (mposInWorld.x < 0 || mposInWorld.x >= MapSettings.MapSize || mposInWorld.y < 0 || mposInWorld.y >= MapSettings.MapSize)  
		{ 
			tileCursor.SetActive(false);
			return; 
		}

		tileCursor.transform.position = TileConversion.TileToWorld3D(mposInWorld);
		tileCursor.SetActive(true);
	}

	// MENU
	private void OnToggleMenu()
	{
		menu.ToggleMenu();
	}

}
