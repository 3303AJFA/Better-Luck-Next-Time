using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

namespace Game.DialogueSystem.Visual
{
    public class DialogueVisual : MonoBehaviour
    {
        [Header("Main Visual")]
        [SerializeField] private GameObject MainVisual;
        [Space]
        [SerializeField] private TextMeshProUGUI DialogueText;
        [SerializeField] private Transform ChooseButtonsParent;
        [SerializeField] private GameObject ChooseButton;
        [Space]
        [SerializeField] private Button NextDialogueButton;

        [Header("Player")]
        [SerializeField] private Image PlayerIcon;
        [SerializeField] private CanvasGroup PlayerCanvasGroup;

        [Header("Comanion")]
        [SerializeField] private Image CompanionIcon;
        [SerializeField] private TextMeshProUGUI CompanionName;
        [SerializeField] private CanvasGroup CompanionCanvasGroup;

        private DialogueManager manager => DialogueManager.Instance;

        private void Start()
        {
            HideDialogue();
        }

        public void ShowDialogue(string startDialogueText, string companionName, Sprite companionIcon)
        {
            UIPanelManager.Instance.OpenPanel(MainVisual);

            CompanionIcon.sprite = companionIcon;
            CompanionName.text = companionName;

            SetCompanionCurrent(startDialogueText);
        }

        public void SetPlayerCurrent(List<string> answers)
        {
            foreach (var answer in answers)
            {
                Button button = Instantiate(ChooseButton, ChooseButtonsParent).GetComponent<Button>();
                button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = answer;

                button.onClick.AddListener(() => Answer(answer));
            }

            CompanionCanvasGroup.alpha = 0.4f;
            PlayerCanvasGroup.alpha = 1f;

            NextDialogueButton.interactable = false;

            DialogueText.gameObject.SetActive(false);
            ChooseButtonsParent.gameObject.SetActive(true);
        }
        public void SetCompanionCurrent(string text)
        {
            ClearAnswersVisual();

            CompanionCanvasGroup.alpha = 1;
            PlayerCanvasGroup.alpha = 0.4f;

            DialogueText.text = text;

            NextDialogueButton.interactable = true;

            DialogueText.gameObject.SetActive(true);
            ChooseButtonsParent.gameObject.SetActive(false);
        }

        public void Answer(string text)
        {
            manager.Answer(text);
        }
        public void NextDialogue()
        {
            manager.NextDialogue();
        }
        public void HideDialogue()
        {
            UIPanelManager.Instance.ClosePanel(MainVisual);
        }

        #region Utilities
        private void ClearAnswersVisual()
        {
            int childCount = ChooseButtonsParent.childCount;
            for (int i = childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(ChooseButtonsParent.GetChild(i).gameObject);
            }
        }
        #endregion
    }
}
