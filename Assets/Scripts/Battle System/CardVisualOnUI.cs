using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.BattleSystem.UIVisual
{
    using Cards;

    public class CardVisualOnUI : MonoBehaviour
    {
        [SerializeField] private Image CardIconImage;
        [SerializeField] private TextMeshProUGUI CardNameText;
        [SerializeField] private TextMeshProUGUI CardDescriptionText;
        [Space]
        [SerializeField] private TextMeshProUGUI CardDamageText;
        [Space]
        [SerializeField] private TextMeshProUGUI TakenHealthText;
        [SerializeField] private TextMeshProUGUI GivenEnergyText;

        [Header("Colors")]
        [SerializeField] private Color TakenHealthColor;
        [SerializeField] private Color GivenEnergyColor;

        private AttackCardSO card;

        public void Initialize(AttackCardSO attackCardSO)
        {
            card = attackCardSO;

            CardIconImage.sprite = card.Icon;
            CardNameText.text = card.CardName;
            CardDescriptionText.text = card.CardDescription;

            CardDamageText.text = "Damage: " + card.Damage;

            string TakenHealthColorHEX = ColorUtility.ToHtmlStringRGB(TakenHealthColor);
            string GivenEnergyColorHEX = ColorUtility.ToHtmlStringRGB(GivenEnergyColor);
            TakenHealthText.text = $"<color=#{TakenHealthColorHEX}>-{card.TakenHealth}</color> Health";
            GivenEnergyText.text = $"<color=#{GivenEnergyColorHEX}>+{card.GivenEnergy}</color> Enegry";
        }

        public void UseCard()
        {
            BattleManager.Instance.UseCard(card);

            Destroy(gameObject);
        }
    }
}
