using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

	[SerializeField] private CameraController cameraController;
	[SerializeField] private MapManager mapManager;
	[SerializeField] private MapGenerator mapGenerator;
	[SerializeField] private Tilemap tilemap;
	[SerializeField] private Tooltip tooltip;
	[SerializeField] private Menu menu;
	[SerializeField] private GameObject selectionBox;
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
	}

	private void Start() 
	{
		selectionBox.SetActive(false);
		tileCursor.SetActive(false);
	}

	private void Update() 
	{
		Vector3 mpos = Mouse.current.position.ReadValue();
		tooltip.Hover(GetSelectableUnderCursor(mpos));	
		// UpdateTileCursor();

		if (holdingMouseDown == true) 
		{ 
			if (!Mouse.current.leftButton.isPressed) { OnHoldRelease(); }
			UpdateSelectionBoxSize(); 
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

	private void OnRightClick(Vector3 mpos, InputAction.CallbackContext context)
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Vector3 worldPoint = ray.GetPoint(0);
		Vector2Int tileLoc = TileConversion.WorldToTile(worldPoint);

		tooltip.Select(null);
		selectionBox.SetActive(false);

		print(tilemap.GetTile(new Vector3Int(tileLoc.x, tileLoc.y, 0)));
	}

	private void OnLeftClick(Vector3 mpos, InputAction.CallbackContext context)
	{
		pointLastClicked = mpos;
		tooltip.Select(GetSelectableUnderCursor(mpos)); 
	}

	private void OnLeftClickHold(Vector3 mpos, InputAction.CallbackContext context)
	{
		holdingMouseDown = true;
		selectionBox.transform.position = pointLastClicked;
		selectionBox.SetActive(true);
	}

	private void OnHoldRelease()
	{
		selectionBox.SetActive(false);
		holdingMouseDown = false;
	}

	private void UpdateSelectionBoxSize()
	{
		Vector2 currPos = Mouse.current.position.ReadValue();
		selectionBox.GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(currPos.x - pointLastClicked.x , currPos.y - pointLastClicked.y);
	}

	private void UpdateTileCursor()
	{
		Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
		Vector3 worldPoint = ray.GetPoint(0);
		Vector2Int mposInWorld = TileConversion.WorldToTile(worldPoint);

		if (mposInWorld.x < 0 || mposInWorld.x >= mapGenerator.MapSize || mposInWorld.y < 0 || mposInWorld.y >= mapGenerator.MapSize)  
		{ 
			tileCursor.SetActive(false);
			return; 
		}

		tileCursor.transform.position = TileConversion.TileToWorld3D(mposInWorld);
		tileCursor.SetActive(true);
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

	private void OnToggleMenu()
	{
		menu.ToggleMenu();
	}

}
