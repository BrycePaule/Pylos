using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public SettingsInjecter SettingsInjecter;
	[SerializeField] private Cinemachine.CinemachineVirtualCamera CVCamera;

	[Header("Movement")]
	public int TilesPerStep;
	public bool Boost;
	public float MoveDelay;

	[Header("Zoom")]
	public int ZoomLevel;
	[SerializeField] private int MinZoomLevel;
	[SerializeField] private int MaxZoomLevel;
	[SerializeField] private int ZoomStep;

	private float movetimer;

	private void Start()
	{
		transform.position = TileConversion.TileToWorld3D(new Vector2Int(SettingsInjecter.MapSettings.MapSize / 2, SettingsInjecter.MapSettings.MapSize / 2));
		Zoom(0);
	}

	public void Move(Vector2 move)
	{
		movetimer += Time.deltaTime;

		if (move == Vector2.zero) { return; }

		SetCameraFollow(null);

		if (movetimer >= MoveDelay)
		{
			int dx, dy;
			if (Boost)
			{
				dx = (int) (Mathf.RoundToInt(move.x) * TilesPerStep * (int) (ZoomLevel / 10) * SettingsInjecter.GameSettings.CameraBoostScrollSpeed);
				dy = (int) (Mathf.RoundToInt(move.y) * TilesPerStep * (int) (ZoomLevel / 10) * SettingsInjecter.GameSettings.CameraBoostScrollSpeed);
			}
			else
			{
				dx = (int) (Mathf.RoundToInt(move.x) * TilesPerStep * (int) (ZoomLevel / 10) * SettingsInjecter.GameSettings.CameraScrollSpeed);
				dy = (int) (Mathf.RoundToInt(move.y) * TilesPerStep * (int) (ZoomLevel / 10) * SettingsInjecter.GameSettings.CameraScrollSpeed);
			}

			transform.position += new Vector3(dx, dy, 0);
			movetimer = 0;
		}
	}

	public void MoveTo(Vector3 loc)
	{
		transform.position = loc;
	}

	public void Zoom(float value)
	{
		ZoomLevel = (int) Mathf.Clamp(ZoomLevel + (-Mathf.Sign(value) * ZoomStep), MinZoomLevel, MaxZoomLevel);
		CVCamera.m_Lens.OrthographicSize = ZoomLevel;
	}

	public void SetCameraFollow(GameObject obj)
	{
		if (obj == null)
		{
			transform.position = CVCamera.m_Follow.position;
			CVCamera.m_Follow = transform;
		}
		else
		{
			CVCamera.m_Follow = obj.transform;
		}
	}
}
