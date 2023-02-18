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

        private AttackCardSO card;

        public void Initialize(AttackCardSO attackCardSO)
        {
            card = attackCardSO;

            CardIconImage.sprite = card.Icon;
            CardNameText.text = card.CardName;
            CardDescriptionText.text = card.CardDescription;
        }

        public void UseCard()
        {
            BattleManager.Instance.UseCard(card);
        }
    }
}
