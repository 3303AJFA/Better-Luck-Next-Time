using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Inventory
{
    using BattleSystem.Cards;
    using Utilities;
    using Input;

    public class PlayerInventory : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private GameObject InventoryPanel;
        [SerializeField] private Transform CardsVisualInventory;
        [SerializeField] private InventoryCardVisual CardVisual;

        [Header("Inventory")]
        [SerializeField, NaughtyAttributes.Expandable] private CardsListSO CardsList;

        private bool isOpened = false;
        private string savePath;
        private CardInventory cardInventory;
        private List<InventoryCardVisual> currentCardVisuals = new List<InventoryCardVisual>();

        private void Awake()
        {
            savePath = $"{Application.dataPath}/";

            Load();
        }

        private void Start()
        { 
            GameInput.Instance.inputActions.Player.Inventory.performed += EnableInventory;
            GameInput.Instance.inputActions.UI.CloseInventory.performed += EnableInventory;

            InitializeVisual();
        }

        #region Visual
        private void InitializeVisual()
        {
            foreach (var card in cardInventory.Cards)
            {
                AddCardVisual(card);
            }
        }
        private InventoryCardVisual GetCardVisual(CardSO card)
        {
            foreach (var visual in currentCardVisuals)
            {
                if (visual.card == card)
                    return visual;
            }
            return null;
        }
        public void AddCardVisual(CardSO card)
        {
            InventoryCardVisual visual = Instantiate(CardVisual.gameObject, CardsVisualInventory).GetComponent<InventoryCardVisual>();
            visual.Initialize(card);
            currentCardVisuals.Add(visual);
        }

        private void EnableInventory(InputAction.CallbackContext ctx)
        {
            if(UIPanelManager.Instance.SomethinkIsOpened())
            {
                if (UIPanelManager.Instance.currentOpenedPanel != InventoryPanel)
                {
                    return;
                }
            }

            isOpened = !isOpened;

            if(isOpened)
            {
                UIPanelManager.Instance.OpenPanel(InventoryPanel);
            }
            else
            {
                UIPanelManager.Instance.ClosePanel(InventoryPanel);
            }
        }
        #endregion

        #region Inventory Interaction
        public void AddCard(CardSO card)
        {
            cardInventory.AddCard(card);
            AddCardVisual(card);

            Save();
        }
        public void RemoveCard(CardSO card)
        {
            InventoryCardVisual visual = GetCardVisual(card);
            if(visual != null)
            {
                currentCardVisuals.Remove(visual);
                Destroy(visual.gameObject);
            }

            cardInventory.RemoveCard(card);

            Save();
        }
        #endregion

        #region Save/Load
        private void Save()
        {
            SaveDataUtility.SaveData(cardInventory, SaveDataUtility.CARD_INVENTORY_FILENAME, savePath);
        }
        private void Load()
        {
            cardInventory = (CardInventory)SaveDataUtility.LoadData<CardInventory>(savePath, SaveDataUtility.CARD_INVENTORY_FILENAME, new CardInventory(CardsList.AllAttackCards));
        }
        #endregion

        private void OnDisable()
        {
            GameInput.Instance.inputActions.Player.Inventory.performed -= EnableInventory;
            GameInput.Instance.inputActions.UI.CloseInventory.performed -= EnableInventory;
        }
    }

    [System.Serializable]
    public class CardInventory
    {
        public int CardsAmount { get { return Cards.Count; } }

        public List<CardSO> Cards;

        public CardInventory(List<CardSO> cardsOnInitialize = null)
        {
            if (cardsOnInitialize != null)
            {
                Cards = cardsOnInitialize;

                CheckCardsForNull();
            }
            else
            {
                Cards = new List<CardSO>();
            }
        }

        private void CheckCardsForNull()
        {
            List<CardSO> elementsToRemove = new List<CardSO>();

            foreach (var card in Cards)
            {
                if (card == null)
                    elementsToRemove.Add(card);
            }

            foreach (var cardToRemove in elementsToRemove)
            {
                Cards.Remove(cardToRemove);
            }
        }

        public void RemoveCard(CardSO card)
        {
            if(Cards.Contains(card))
                Cards.Remove(card);
        }
        public void AddCard(CardSO card)
        {
            Cards.Add(card);
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
