using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Player.Inventory
{
    using BattleSystem.Cards;

    public class InventoryCardVisual : MonoBehaviour
    {
        [System.Serializable]
        private struct QualityColor
        {
            public Color Color;
            public CardQuality Quality;
        }

        [Header("Card variables")]
        [SerializeField] private Image CardBGImage;
        [SerializeField] private TextMeshProUGUI CardNameText;
        [SerializeField] private TextMeshProUGUI CardDescriptionText;
        [Space]
        [SerializeField] private TextMeshProUGUI CardDamageText;
        [Space]
        [SerializeField] private TextMeshProUGUI HealthText;
        [SerializeField] private TextMeshProUGUI EnergyText;

        [Header("Colors")]
        [SerializeField] private Color PositiveColor = Color.white;
        [SerializeField] private Color NegativeColor = Color.white;
        [SerializeField] private Color NeutralColor = Color.white;
        [Space]
        [SerializeField] private List<QualityColor> QualityColors = new List<QualityColor>();

        public CardSO card { get; private set; }

        public void Initialize(CardSO attackCardSO)
        {
            card = attackCardSO;

            // Visual
            SetCardFrontVisual();
            SetCardAttackVariablesVisual();
            SetCardQualityVisual();
        }

        #region Visual initializing
        private void SetCardFrontVisual()
        {
            CardBGImage.sprite = card.CardImage;
            CardNameText.text = card.CardName;
            CardDescriptionText.text = card.CardDescription;
        }
        private void SetCardAttackVariablesVisual()
        {
            CardDamageText.text = "Damage: " + card.Damage;

            // Set Colors
            string positive = ColorUtility.ToHtmlStringRGB(PositiveColor);
            string negative = ColorUtility.ToHtmlStringRGB(NegativeColor);
            string neutral = ColorUtility.ToHtmlStringRGB(NeutralColor);

            // Health Income
            string healthIncomeText_text = "";
            if (card.HealthIncome == 0)
                healthIncomeText_text = $"<color=#{neutral}>{card.HealthIncome}</color> Health";
            else if (card.HealthIncome > 0)
                healthIncomeText_text = $"<color=#{positive}>+{card.HealthIncome}</color> Health";
            else if (card.HealthIncome < 0)
                healthIncomeText_text = $"<color=#{negative}>{card.HealthIncome}</color> Health";

            // Enegry Income
            string energyIncomeText_text = "";
            if (card.EnergyIncome == 0)
                energyIncomeText_text = $"<color=#{neutral}>{card.EnergyIncome}</color> Energy";
            else if (card.EnergyIncome > 0)
                energyIncomeText_text = $"<color=#{positive}>+{card.EnergyIncome}</color> Energy";
            else if (card.EnergyIncome < 0)
                energyIncomeText_text = $"<color=#{negative}>{card.EnergyIncome}</color> Energy";

            // Apply Energy/Health income
            HealthText.text = healthIncomeText_text;
            EnergyText.text = energyIncomeText_text;
        }
        private void SetCardQualityVisual()
        {
            foreach (var qualityData in QualityColors)
            {
                if (qualityData.Quality == card.Quality)
                {
                    CardNameText.color = qualityData.Color;
                }
            }
        }
        #endregion
    }
}
