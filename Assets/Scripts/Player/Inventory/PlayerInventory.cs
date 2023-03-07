using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Inventory
{
    using Visual;
    using BattleSystem.Cards;
    using Utilities;
    using Input;

    public class PlayerInventory : MonoBehaviour
    {
        public static PlayerInventory Instance;

        [Header("Visual")]
        [SerializeField] private InventoryVisual Visual;
        [SerializeField] private GameObject InventoryPanel;
        [SerializeField] private Transform CardsVisualInventory;
        [SerializeField] private InventoryCardVisual CardVisual;

        [Header("Inventory")]
        public CardInventory CardInventory;
        [SerializeField] private List<Item> items = new List<Item>();
        [SerializeField, NaughtyAttributes.Expandable] private CardsListSO CardsList;

        private bool isOpened = false;
        private string savePath;

        private void Awake()
        {
            Instance = this;

            savePath = $"{Application.dataPath}/";

            Load();
        }

        private void Start()
        { 
            Visual.InitializeVisual(CardInventory);
        }

        #region Inventory Interaction
        #region Card
        public void AddCard(CardSO card)
        {
            CardInventory.Cards.Add(card);
            Visual.AddVisual(card);

            Save();
        }
        public void RemoveCard(CardSO card)
        {
            if (!CardInventory.Cards.Contains(card))
                return;

            CardInventory.Cards.Remove(card);

            Save();
        }
        #endregion

        #region Item
        public void AddItem(Item item)
        {
            CardInventory.Items.Add(item);
            Visual.AddVisual(item);

            Save();
        }
        public void RemoveItem(Item item)
        {
            if (!CardInventory.Items.Contains(item))
                return;

            CardInventory.Items.Remove(item);

            Save();
        }
        #endregion
        #endregion

        #region Save/Load
        private void Save()
        {
            SaveDataUtility.SaveData(CardInventory, SaveDataUtility.CARD_INVENTORY_FILENAME, savePath);
        }
        private void Load()
        {
            CardInventory = (CardInventory)SaveDataUtility.LoadData<CardInventory>(
                savePath, 
                SaveDataUtility.CARD_INVENTORY_FILENAME, 
                new CardInventory(CardsList.AllAttackCards, items)
                );
        }
        #endregion
    }

    [System.Serializable]
    public class CardInventory
    {
        public int CardsAmount { get { return Cards.Count; } }

        public List<CardSO> Cards;
        public List<Item> Items;

        public CardInventory(List<CardSO> cardsOnInitialize = null, List<Item> itemsOnInitialize = null)
        {
            if (cardsOnInitialize != null)
            {
                Cards = cardsOnInitialize;
                CheckListForNull(ref cardsOnInitialize);
            }
            else
            {
                Cards = new List<CardSO>();
            }

            if (itemsOnInitialize != null)
            {
                Items = itemsOnInitialize;
                CheckListForNull(ref itemsOnInitialize);
            }
            else
            {
                itemsOnInitialize = new List<Item>();
            }
        }

        private void CheckListForNull<T>(ref List<T> list)
        {
            List<T> elementsToRemove = new List<T>();

            foreach (var element in list)
            {
                if (element == null)
                    elementsToRemove.Add(element);
            }

            foreach (var element in elementsToRemove)
            {
                list.Remove(element);
            }
        }

        public CardSO GetRandomCard()
        {
            CardSO card = null;
            if (CardsAmount > 0)
            {
                card = Cards[Random.Range(0, Cards.Count)];
            }
            return card;
        }
    }
}
