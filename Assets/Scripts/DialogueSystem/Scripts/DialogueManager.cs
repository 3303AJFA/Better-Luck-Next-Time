using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DialogueSystem
{
    using Visual;
    using DS.ScriptableObjects;
    using DS.Enumerations;

    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance;

        [Header("Visual")]
        [SerializeField] private DialogueVisual Visual;

        private DSDialogueContainerSO currentDialogueContainer;
        private DSDialogueSO currentDialogue;

        private void Awake()
        {
            Instance = this;
        }

        public void StartDialogue(DSDialogueContainerSO dialogue, string name, Sprite icon)
        {
            currentDialogueContainer = dialogue;

            currentDialogue = currentDialogueContainer.GetStartDialogue();

            Visual.ShowDialogue(currentDialogue.Text, name, icon);

            PauseManager.Instance.PauseGame(true, false);
        }

        public void NextDialogue()
        {
            if(currentDialogue.DialogueType == DSDialogueType.SingleChoice)
            {
                DSDialogueSO next = currentDialogue.Choices[0].NextDialogue;
                if (next != null)
                {
                    currentDialogue = next;

                    ShowCurrentDialogue();
                }
                else
                {
                    EndDialogue();
                }
            }
            else if(currentDialogue.DialogueType == DSDialogueType.MultipleChoice)
            {
                List<string> answers = new List<string>();
                foreach (var choice in currentDialogue.Choices)
                {
                    answers.Add(choice.Text);
                }

                Visual.SetPlayerCurrent(answers);
            }
        }
        private void ShowCurrentDialogue()
        {
            Visual.SetCompanionCurrent(currentDialogue.Text);
        }

        public void Answer(string text)
        {
            foreach (var choice in currentDialogue.Choices)
            {
                if(choice.Text == text)
                {
                    currentDialogue = choice.NextDialogue;

                    ShowCurrentDialogue();
                }
            }
        }
        public void EndDialogue()
        {
            Visual.HideDialogue();

            PauseManager.Instance.PauseGame(false, false);
        }
    }
}
