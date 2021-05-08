using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject defaultCamera;
    [SerializeField] private MapManager mapManager;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private GameObject selectionBox;

    private InputAction _playerMovement;
    private InputAction _playerLeftClick;
    private InputAction _playerLeftClickHold;
    private InputAction _playerRightClick;
    private InputAction _cameraZoom;

    private Rigidbody2D _playerRB;
    private PlayerMovement playerMovement;
    private Vector2 pointLastClicked;
    private bool holdingMouseDown;

    private void OnEnable()
    {
        _playerMovement.Enable();
        _playerLeftClick.Enable();
        _playerLeftClickHold.Enable();
        _playerRightClick.Enable();
        _cameraZoom.Enable();
    }

    private void Awake()
    {
        playerMovement = player.GetComponentInChildren<PlayerMovement>(); 
        _playerRB = player.GetComponent<Rigidbody2D>();

        PlayerControls _playerControls = new PlayerControls();

        _playerMovement = _playerControls.Player.Movement;

        _playerLeftClick = _playerControls.Player.LeftClick;
        _playerLeftClick.performed += OnLeftClick;

        _playerLeftClickHold = _playerControls.Player.LeftClickHold;
        _playerLeftClickHold.performed += OnLeftClickHold;

        _playerRightClick = _playerControls.Player.RightClick;
        _playerRightClick.performed += OnRightClick;

        _cameraZoom = _playerControls.Camera.Zoom;
        _cameraZoom.performed += OnCameraZoom;
    }

	private void Start() 
	{
		selectionBox.SetActive(false);
	}

	private void Update() 
	{
        tooltip.Hover(Mouse.current.position.ReadValue());

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

        playerMovement.Move(moveDir);
    }

    private void OnCameraZoom(InputAction.CallbackContext context)
    {
        Cinemachine.CinemachineVirtualCamera cvcam = defaultCamera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        float scroll = context.ReadValue<Vector2>().y;
        if (scroll < 0) 
        {
            if (cvcam.m_Lens.OrthographicSize >= 50) { return; }
            cvcam.m_Lens.OrthographicSize += 1;
        } else if (scroll > 0)
        {
            if (cvcam.m_Lens.OrthographicSize <= 10) { return; }
            cvcam.m_Lens.OrthographicSize -= 1;
        }
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 worldPoint = ray.GetPoint(0);
        Vector2Int cellLocation = TileConversion.WorldToTile(worldPoint);

        print(
            mapManager.GetTile(cellLocation).GroundType 
            + " | " + cellLocation 
            + " | Walkable: " 
            + mapManager.IsWalkable(cellLocation)
        );

		selectionBox.SetActive(false);
    }

	private void OnLeftClick(InputAction.CallbackContext context)
	{
		print("clicked");
		pointLastClicked = Mouse.current.position.ReadValue();
	}

	private void OnLeftClickHold(InputAction.CallbackContext context)
	{
		print("held");
		holdingMouseDown = true;
		selectionBox.transform.position = pointLastClicked;
		selectionBox.SetActive(true);
	}

	private void OnHoldRelease()
	{
		print("released");

		selectionBox.SetActive(false);
		holdingMouseDown = false;;
	}

	private void UpdateSelectionBoxSize()
	{
		Vector2 currPos = Mouse.current.position.ReadValue();
		selectionBox.GetComponentInChildren<Image>().rectTransform.sizeDelta = new Vector2(currPos.x - pointLastClicked.x , currPos.y - pointLastClicked.y);
	}
}
