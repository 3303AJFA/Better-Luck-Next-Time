using System.Collections.Generic;
using UnityEngine;

namespace DS.ScriptableObjects
{
    using Game.Player.Inventory;
    using Data;
    using Enumerations;

    public class DSDialogueSO : ScriptableObject
    {
        [field: SerializeField] public string DialogueName { get; set; }
        [field: SerializeField] [field: TextArea()] public string Text { get; set; }
        [field: SerializeField] public List<DSDialogueChoiceData> Choices { get; set; }
        [field: SerializeField] public Item Item { get; set; }
        [field: SerializeField] public DSDialogueType DialogueType { get; set; }
        [field: SerializeField] public bool IsStartingDialogue { get; set; }

        public void Initialize(string dialogueName, string text, List<DSDialogueChoiceData> choices, Item item, DSDialogueType dialogueType, bool isStartingDialogue)
        {
            DialogueName = dialogueName;
            Text = text;
            Choices = choices;
            Item = item;
            DialogueType = dialogueType;
            IsStartingDialogue = isStartingDialogue;
        }
    }
}