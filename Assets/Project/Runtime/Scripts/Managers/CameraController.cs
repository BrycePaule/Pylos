using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public MapSettings MapSettings;
	[SerializeField] private Cinemachine.CinemachineVirtualCamera CVCamera;

	[Header("Movement")]
	public float MoveDelay;
	public int TilesPerStep;

	[Header("Zoom")]
	public int ZoomLevel;
	[SerializeField] private int MinZoomLevel;
	[SerializeField] private int MaxZoomLevel;
	[SerializeField] private int ZoomStep;

	private float _moveTimer;

	private void Start()
	{
		transform.position = TileConversion.TileToWorld3D(new Vector2Int(MapSettings.MapSize / 2, MapSettings.MapSize / 2));
		Zoom(0);
	}

	public void Move(Vector2 move)
	{
		_moveTimer += Time.deltaTime;

		{
			int dx = Mathf.RoundToInt(move.x) * TilesPerStep * (int) (ZoomLevel / 10);
			int dy = Mathf.RoundToInt(move.y) * TilesPerStep * (int) (ZoomLevel / 10);

			transform.position += new Vector3(dx, dy, 0);
			_moveTimer = 0;
		}
	}

	public void Zoom(float value)
	{
		ZoomLevel = (int) Mathf.Clamp(ZoomLevel + (-Mathf.Sign(value) * ZoomStep), MinZoomLevel, MaxZoomLevel);
		CVCamera.m_Lens.OrthographicSize = ZoomLevel;
	}
}
