using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    using Input;

    public class UIPanelManager : MonoBehaviour
    {
        public static UIPanelManager Instance;

        public GameObject currentOpenedPanel { get; private set; }

        [System.Serializable]
        public struct Panels
        {
            public string name;
            public GameObject panel_obj;
        }
        [Header("OpenClose panels")]
        public List<Panels> panels = new List<Panels>();

        private void Awake() => Instance = this;

        private void EnablePanel(GameObject panel, bool enabled, bool stopTime = true)
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].panel_obj.SetActive(false);
            }

            foreach (var item in panels)
            {
                if (item.panel_obj == panel)
                {
                    panel.SetActive(enabled);
                    currentOpenedPanel = enabled ? panel : null;
                    return;
                }
            }

            panel.SetActive(enabled);
        }

        public void ClosePanel(GameObject panel)
        {
            EnablePanel(panel, false, false);

            GameInput.Instance.EnablePlayerActionMap();
        }

        public void OpenPanel(GameObject panel, bool enableUI = true, bool stopTime = true)
        {
            currentOpenedPanel = panel;
            EnablePanel(panel, true, stopTime);

            if(enableUI)
                GameInput.Instance.EnableUIPlayerActionMap();
        }

        public void CloseAllPanel()
        {
            for (int i = 0; i < panels.Count; i++)
            {
                panels[i].panel_obj.SetActive(false);
            }
        }

        public bool SomethinkIsOpened()
        {
            foreach (var panel in panels)
            {
                if(panel.panel_obj.activeSelf)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
