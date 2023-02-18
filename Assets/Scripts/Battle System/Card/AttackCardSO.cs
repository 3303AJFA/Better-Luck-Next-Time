using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.BattleSystem.Cards
{
    public abstract class AttackCardSO : ScriptableObject
    {
        // Variables
        [ShowAssetPreview] public Sprite Icon;
        [Space]
        public string CardName;
        [ResizableTextArea] public string CardDescription = "Stadart attack card";
        [Space]
        public float Damage;

        // Logic
        public abstract void Activate();
    }
}
