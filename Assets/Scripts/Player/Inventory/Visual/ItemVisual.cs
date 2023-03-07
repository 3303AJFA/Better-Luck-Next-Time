using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Player.Inventory.Visual
{
    public class ItemVisual : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI ItemNameText;
        [SerializeField] private Image ItemIconImage;

        public Item Item { get; private set; }

        public void Initialize(Item item)
        {
            ItemNameText.text = item.name;
            ItemIconImage.sprite = item.Icon;

            Item = item;
        }
    }
}
