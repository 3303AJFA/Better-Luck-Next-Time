using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Tilemap MovementMap;
        [SerializeField] private float TileDistance = 1.2f;

        private Transform myTransform;
        Vector3 dir = Vector3.zero;

        private void Start()
        {
            myTransform = transform;
            dir = myTransform.position;
        }

        void Update()
        {
            Vector3 playerPos = new Vector3(myTransform.position.x, 0, myTransform.position.z);

            if (Input.GetKeyDown(KeyCode.W))
            {
                dir = playerPos + Vector3.forward * TileDistance;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                dir = playerPos - Vector3.forward * TileDistance;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                dir = playerPos + Vector3.left * TileDistance;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                dir = playerPos - Vector3.left * TileDistance;
            }

            Vector3Int tilePos = MovementMap.WorldToCell(dir);
            if (MovementMap.HasTile(tilePos))
            {
                Vector3 targetPos = MovementMap.CellToWorld(tilePos);
                myTransform.position = targetPos;
            }
        }
    }
}
