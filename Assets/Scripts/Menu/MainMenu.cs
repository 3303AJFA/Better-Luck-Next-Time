using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.Menu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField, Scene] private int StartGameScene;

        private LoadSceneUtility sceneUtility;

        private void Start()
        {
            sceneUtility = FindObjectOfType<LoadSceneUtility>();
        }

        public void StartGame()
        {
            sceneUtility.LoadScene(StartGameScene);
        }
    }
}
