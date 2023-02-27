using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

namespace Game
{
    public class LoadSceneUtility : MonoBehaviour
    {
        [SerializeField, Scene] private int MenuSceneId;

        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }
        public void LoadScene(string index)
        {
            SceneManager.LoadScene(index);
        }

        public void ReturnToMenu()
        {
            LoadScene(MenuSceneId);
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
