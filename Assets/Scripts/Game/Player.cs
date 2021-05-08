using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	[SerializeField] MapManager mapManager;

    private void Start()
    {
		transform.position = TileConversion.TileToWorld3D(mapManager.PlayerSpawn);
		transform.GetComponentInChildren<PlayerMovement>().TileLoc = mapManager.PlayerSpawn;
    }

}
