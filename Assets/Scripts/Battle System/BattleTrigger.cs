using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

namespace Game.BattleSystem
{
    using Player;

    public class BattleTrigger : MonoBehaviour
    {
        [SerializeField, Tag] private string PlayerTag = "Player";
        [SerializeField, Scene] private int BattleSceneId = 1;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag(PlayerTag))
            {
                SceneManager.LoadScene(BattleSceneId);
            }
        }
    }
}
