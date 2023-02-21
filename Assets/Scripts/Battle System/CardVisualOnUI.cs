using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

namespace Game.BattleSystem.UIVisual
{
    using Cards;

    public class CardVisualOnUI : MonoBehaviour
    {
        [Header("Card variables")]
        [SerializeField] private Image CardBGImage;
        [SerializeField] private TextMeshProUGUI CardNameText;
        [SerializeField] private TextMeshProUGUI CardDescriptionText;
        [Space]
        [SerializeField] private TextMeshProUGUI CardDamageText;
        [Space]
        [SerializeField] private TextMeshProUGUI HealthText;
        [SerializeField] private TextMeshProUGUI EnergyText;

        [Header("Animator")]
        [SerializeField] private Animator Anim;
        private string Anim_disappearanceTrigger;

        [Header("Colors")]
        [SerializeField] private Color PositiveColor = Color.white;
        [SerializeField] private Color NegativeColor = Color.white;
        [SerializeField] private Color NeutralColor = Color.white;

        private AttackCardSO card;

        public void Initialize(AttackCardSO attackCardSO)
        {
            card = attackCardSO;

            CardBGImage.sprite = card.CardView;
            CardNameText.text = card.CardName;
            CardDescriptionText.text = card.CardDescription;

            CardDamageText.text = "Damage: " + card.Damage;

            // Set Colors
            string positive = ColorUtility.ToHtmlStringRGB(PositiveColor);
            string negative = ColorUtility.ToHtmlStringRGB(NegativeColor);
            string neutral = ColorUtility.ToHtmlStringRGB(NeutralColor);

            // Health Income
            string healthIncomeText_text = "";
            if (card.HealthIncome == 0)
                healthIncomeText_text = $"<color=#{neutral}>{card.HealthIncome}</color> Health";
            else if(card.HealthIncome > 0)
                healthIncomeText_text = $"<color=#{positive}>+{card.HealthIncome}</color> Health";
            else if(card.HealthIncome < 0)
                healthIncomeText_text = $"<color=#{negative}>{card.HealthIncome}</color> Health";

            // Enegry Income
            string energyIncomeText_text = "";
            if (card.EnergyIncome == 0)
                energyIncomeText_text = $"<color=#{neutral}>{card.EnergyIncome}</color> Energy";
            else if(card.EnergyIncome > 0)
                energyIncomeText_text = $"<color=#{positive}>+{card.EnergyIncome}</color> Energy";
            else if (card.EnergyIncome < 0)
                energyIncomeText_text = $"<color=#{negative}>{card.EnergyIncome}</color> Energy";

            // Apply Energy/Health income
            HealthText.text = healthIncomeText_text;
            EnergyText.text = energyIncomeText_text;

            // Animatior
            Anim.runtimeAnimatorController = card.CardAnimation;

            Anim_disappearanceTrigger = card.Anim_disappearanceTrigger;

            Anim.enabled = true;
        }

        public void UseCard()
        {
            if(BattleManager.Instance.CurrentTurn == Turn.Player)
            {
                BattleManager.Instance.UseCard(card);

                Anim.SetTrigger(Anim_disappearanceTrigger);
            }
        }

        private void DestroyCard() // for animation
        {
            Destroy(gameObject);
        }
    }
}
