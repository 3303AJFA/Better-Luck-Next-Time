using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.BattleSystem
{
    public abstract class CardSO : ScriptableObject
    {
        // Variables
        [ShowAssetPreview] public Sprite Icon;
        [ResizableTextArea] public string Description;
        [Space]
        public float Damage;

        // Logic
        public abstract void Activate();
    }
}
