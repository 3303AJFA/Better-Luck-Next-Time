using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

namespace Game.Player
{
    using Input;

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject Visual;
        [Space]
        [SerializeField] private Tilemap MovementMap;
        [SerializeField] private float TileDistance = 1.2f;

        private Transform myTransform;
        private Vector3 direction;
        private Camera cam;

        private void Start()
        {
            myTransform = transform;
            direction = myTransform.position;
            cam = Camera.main;

            Visual.transform.LookAt(cam.transform.position);

            GameInput.Instance.inputActions.Player.Movement.performed += OnMoveInputPressed;

            PlayerMove(Vector2.zero, new Vector3(myTransform.position.x, 0, myTransform.position.z));
        }

        private void OnMoveInputPressed(InputAction.CallbackContext context)
        {
            Vector3 playerPos = new Vector3(myTransform.position.x, 0, myTransform.position.z);

            PlayerMove(context.ReadValue<Vector2>(), playerPos);
        }

        private void PlayerMove(Vector2 dir, Vector3 playerPos)
        {
            Vector2 invertedDir = new Vector2(dir.y, dir.x);

            direction = playerPos + new Vector3(invertedDir.y, 0, invertedDir.x) * TileDistance;

            Vector3Int tilePos = MovementMap.WorldToCell(direction);
            if (MovementMap.HasTile(tilePos))
            {
                Vector3 targetPos = MovementMap.CellToWorld(tilePos);
                myTransform.position = targetPos;
            }
        }
    }
}
