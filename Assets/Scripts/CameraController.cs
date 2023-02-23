using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform TargetTransform;
        [SerializeField] private LayerMask TargetLayer;
        [SerializeField] private float MinDistance = 4f;

        private Transform myTransform;

        private void Start()
        {
            TargetTransform = FindObjectOfType<PlayerController>().transform;
            myTransform = transform;
        }

        private void Update()
        {
            bool playerInViewZone = Physics.BoxCast(myTransform.position, new Vector3(MinDistance, MinDistance), transform.forward, transform.localRotation, 100f, TargetLayer);

            if(!playerInViewZone)
            {
                Vector2 camPositon = new Vector2(myTransform.position.z, myTransform.position.x);
                Vector2 targetPosition = new Vector2(TargetTransform.position.z, TargetTransform.position.x);
                Vector2 direction = camPositon - targetPosition;
                Debug.Log(direction);
                Vector3 targetCamPosition = new Vector3(direction.x, 0, direction.y);

                myTransform.position = targetCamPosition;
            }
        }
    }
}
