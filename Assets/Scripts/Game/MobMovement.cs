using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
	public MapManager MapManager;
	public MapGenerator MapGenerator;
	public Vector2Int TileLoc;

	public float MoveDelay;
	public float TilesPerStep; 
	public int MeanderDistance;

	private Vector2Int _targetLoc;
	private Rigidbody2D _mobRB;
	private MobBase _mobBase;
	private float _moveTimer;
	private AStar _aStar;

	private void Awake() 
	{
		_mobRB = GetComponentInParent<Rigidbody2D>();
		_mobBase = GetComponentInParent<MobBase>();
		_aStar = GetComponent<AStar>();
	}

	private void FixedUpdate() 
	{
		Move();
	}

	private void Move()
	{
		_moveTimer += Time.deltaTime;

		if (TileLoc == _targetLoc || _targetLoc == Vector2.zero) { _targetLoc = SelectNewTargetLocation(); }

		if (_moveTimer >= MoveDelay)
		{
			int dx = (int) Mathf.Clamp(_targetLoc.x - TileLoc.x, -1, 1);
			int dy = (int) Mathf.Clamp(_targetLoc.y - TileLoc.y, -1, 1);

			Vector2Int dest = TileLoc + new Vector2Int(dx, dy);
			GroundTileData destTile = MapManager.GetTile(dest);

			if (_mobBase.CanWalk)
			{
				if (destTile.IsWalkable)
				{
					_mobRB.MovePosition(TileConversion.TileToWorld2D(dest));
					TileLoc = dest;
					_moveTimer = 0;
					return;
				}
			}

			if (_mobBase.CanSwim)
			{
				if (destTile.IsSwimmable)
				{
					_mobRB.MovePosition(TileConversion.TileToWorld2D(dest));
					TileLoc = dest;
					_moveTimer = 0;
					return;
				}
			}
			// print("i can't move");
		}
	}

	public Vector2Int SelectNewTargetLocation()
	{
		return new Vector2Int(
			Mathf.Clamp(TileLoc.x + (int) Random.Range(-MeanderDistance, MeanderDistance), 0, MapGenerator.MapSize),
			Mathf.Clamp(TileLoc.y + (int) Random.Range(-MeanderDistance, MeanderDistance), 0, MapGenerator.MapSize));
	}

	public void RandomiseMovementTick()
	{
		_moveTimer += Random.Range(0, MoveDelay);
	}
}
