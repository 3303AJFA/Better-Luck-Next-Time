using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using NaughtyAttributes;

namespace Game.Player
{
    using Input;

    public class PlayerController : MonoBehaviour
    {
        [Header("Visual")]
        [SerializeField] private Animator Anim;
        [SerializeField, AnimatorParam("Anim")] private string Anim_moveTrigger;
        [Space]
        [SerializeField] private GameObject Visual;

        [Header("Movement")]
        [SerializeField] private float timeBtwMoving = 0.3f;
        [SerializeField] private Tilemap MovementMap;
        [SerializeField] private float TileDistance = 1f;

        private Transform myTransform;
        private Vector3 direction;
        private Camera cam;
        private bool moveButtonIsPressed = false;
        private float currentTimeBtwMoving = 0;
        private bool canMove = true;

        private void Start()
        {
            myTransform = transform;
            direction = myTransform.position;
            cam = Camera.main;

            Visual.transform.LookAt(cam.transform.position);

            GameInput.Instance.inputActions.Player.Movement.performed += MovementPerfomed;
            GameInput.Instance.inputActions.Player.Movement.canceled += MovementCanceled;
            PauseManager.Instance.OnPauseStatusShanged += OnPauseStateChanged;

            MovePlayer(Vector2.zero, new Vector3(myTransform.position.x, 0, myTransform.position.z));
        }
        private void MovementPerfomed(InputAction.CallbackContext context)
        {
            Vector3 playerPos = new Vector3(transform.position.x, 0, transform.position.z);

            MovePlayer(GameInput.Instance.GetMoveVector(), playerPos);
            moveButtonIsPressed = true;
            currentTimeBtwMoving = timeBtwMoving;
        }
        private void MovementCanceled(InputAction.CallbackContext context)
        {
            moveButtonIsPressed = false;
            currentTimeBtwMoving = timeBtwMoving;
        }

        private void Update()
        {
            if (!canMove)
                return;

            if(moveButtonIsPressed)
            {
                if(currentTimeBtwMoving <= 0)
                {
                    Vector3 playerPos = new Vector3(myTransform.position.x, 0, myTransform.position.z);
                    MovePlayer(GameInput.Instance.GetMoveVector(), playerPos);

                    currentTimeBtwMoving = timeBtwMoving;
                }
                else
                {
                    currentTimeBtwMoving -= Time.deltaTime;
                }
            }
        }

        private void MovePlayer(Vector2 dir, Vector3 playerPos)
        {
            Vector2 invertedDir = new Vector2(dir.y, dir.x);

            direction = playerPos + new Vector3(invertedDir.y, 0, invertedDir.x) * TileDistance;

            Vector3Int tilePos = MovementMap.WorldToCell(direction);
            if (MovementMap.HasTile(tilePos))
            {
                Vector3 targetPos = MovementMap.CellToWorld(tilePos);
                if(myTransform.position != targetPos)
                {
                    myTransform.position = targetPos;
                    Anim.SetTrigger(Anim_moveTrigger);
                }
            }
        }

        private void OnPauseStateChanged(bool inPause)
        {
            canMove = !inPause;
        }

        private void OnDisable()
        {
            GameInput.Instance.inputActions.Player.Movement.performed -= MovementPerfomed;
            GameInput.Instance.inputActions.Player.Movement.canceled -= MovementCanceled;
            PauseManager.Instance.OnPauseStatusShanged -= OnPauseStateChanged;
        }
    }
}
