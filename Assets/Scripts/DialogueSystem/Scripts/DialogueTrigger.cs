using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.DialogueSystem
{
    using DS.ScriptableObjects;

    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField, Tag] private string PlayerTag = "Player";
        [Space]
        [SerializeField] private DSDialogueContainerSO Dialogue;
        [SerializeField] private string Name;
        [SerializeField] private Sprite Icon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(PlayerTag))
            {
                DialogueManager.Instance.StartDialogue(Dialogue, Name, Icon);
            }
        }
    }
}
