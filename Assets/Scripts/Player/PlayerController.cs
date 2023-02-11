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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0.5f, 0.25f) * TileDistance)); // forward

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (transform.position + new Vector3(0.5f, -0.25f) * TileDistance)); // right

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, (transform.position - new Vector3(0.5f, 0.25f) * TileDistance)); // back

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, (transform.position - new Vector3(0.5f, -0.25f) * TileDistance)); // left
        }

        private void Start()
        {
            myTransform = transform;
            dir = myTransform.position;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                dir = myTransform.position + new Vector3(0.5f, 0.25f) * TileDistance;
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                dir = myTransform.position - new Vector3(0.5f, 0.25f) * TileDistance;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                dir = myTransform.position - new Vector3(0.5f, -0.25f) * TileDistance;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                dir = myTransform.position + new Vector3(0.5f, -0.25f) * TileDistance;
            }

            Vector3Int tilePos = MovementMap.WorldToCell(dir);
            if(MovementMap.HasTile(tilePos))
            {
                Vector3 targetPos = MovementMap.CellToWorld(tilePos);
                myTransform.position = targetPos;
            }
        }
    }
}
