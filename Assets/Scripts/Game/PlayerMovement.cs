using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{

    public float MoveDelay;
    public int TilesPerStep;
    public Vector2Int TileLoc;

    [Space(10)]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private MapManager mapManager;

    private Rigidbody2D _playerRB;
    private float _moveTimer;

    private void Awake() {
        _playerRB = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 move)
    {
        _moveTimer += Time.deltaTime;

        if (_moveTimer >= MoveDelay)
        {
            int dx = Mathf.RoundToInt(move.x) * TilesPerStep;
            int dy = Mathf.RoundToInt(move.y) * TilesPerStep;
            Vector2Int dest = TileLoc + new Vector2Int(dx, dy);

            if (mapManager.IsWalkable(dest))
            {
                _playerRB.MovePosition(TileConversion.TileToWorld2D(dest));
				TileLoc = dest;
                _moveTimer = 0;;
            }
            else
            {
                return;
            }
        }
    }
}
