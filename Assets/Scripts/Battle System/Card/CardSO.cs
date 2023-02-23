using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Game.BattleSystem.Cards
{
    public enum CardQuality
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public abstract class CardSO : ScriptableObject
    {
        // Variables
        [ShowAssetPreview] public Sprite CardImage;
        [Space]
        public CardQuality Quality = CardQuality.Common;
        public string CardName;
        [ResizableTextArea] public string CardDescription = "Stadart attack card";
        [Space]
        [Tooltip("(Enemy health) - Damage")] public float Damage;
        [Tooltip("(Player health) + HealthIncome")] public float HealthIncome;
        [Tooltip("(Player energy) + EnergyIncome")] public float EnergyIncome;

        [Header("Animator")]
        public RuntimeAnimatorController CardAnimation;
        public string Anim_disappearanceTrigger;

        // Logic
        public abstract void Activate();
    }
}
