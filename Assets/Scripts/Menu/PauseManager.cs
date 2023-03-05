using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    using Input;

    public class PauseManager : MonoBehaviour
    {
        public delegate void StateChanged(bool paused);
        public static PauseManager Instance;

        [Header("UI")]
        [SerializeField] private GameObject PausePanel;

        public StateChanged OnPauseStatusShanged;

        public bool paused { get; private set; }
        private LoadSceneUtility sceneUtility;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            GameInput.Instance.inputActions.Player.Pause.performed += PauseGameFunction;
            GameInput.Instance.inputActions.UI.Close.performed += PauseGameFunction;

            sceneUtility = FindObjectOfType<LoadSceneUtility>();

            PauseGame(false);
        }

        private void PauseGameFunction(InputAction.CallbackContext context)
        {
            if (UIPanelManager.Instance.SomethinkIsOpened())
            {
                if(UIPanelManager.Instance.currentOpenedPanel != PausePanel)
                {
                    return;
                }
            }

            paused = !paused;

            PauseGame(paused);
        }
        public void PauseGame(bool pauseEnabed, bool visualisePanel = true)
        {
            OnPauseStatusShanged?.Invoke(pauseEnabed);

            if(visualisePanel)
            {
                if (pauseEnabed)
                {
                    UIPanelManager.Instance.OpenPanel(PausePanel);
                }
                else
                {
                    UIPanelManager.Instance.ClosePanel(PausePanel);
                }
            }
        }

        public void ReturnToMenu()
        {
            sceneUtility.ReturnToMenu();
        }

        private void OnDisable()
        {
            GameInput.Instance.inputActions.Player.Pause.performed -= PauseGameFunction;
            GameInput.Instance.inputActions.UI.Close.performed -= PauseGameFunction;
        }
    }
}
