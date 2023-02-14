using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.BattleSystem
{
    using Player;

    public class BattleTrigger : MonoBehaviour
    {
        [SerializeField, NaughtyAttributes.Scene] private int BattleSceneId = 1;

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                SceneManager.LoadScene(BattleSceneId);
            }
        }
    }
}
