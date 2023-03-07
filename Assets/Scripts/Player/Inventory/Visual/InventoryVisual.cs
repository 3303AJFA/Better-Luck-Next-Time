using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player.Inventory.Visual
{
    using Input;
    using BattleSystem.Cards;

    public class InventoryVisual : MonoBehaviour
    {
        [SerializeField] private GameObject InventoryPanel;
        [Space]
        [SerializeField] private GameObject CardPanel;
        [SerializeField] private Transform CardsVisualInventory;
        [SerializeField] private InventoryCardVisual CardVisual;
        [Space]
        [SerializeField] private GameObject ItemPanel;
        [SerializeField] private Transform ItemsVisualInventory;
        [SerializeField] private ItemVisual ItemVisual;

        public bool isOpened { get; private set; }

        private CardInventory cardInventory;
        private List<InventoryCardVisual> CurrentCardVisuals = new List<InventoryCardVisual>();
        private List<ItemVisual> CurrentItemVisuals = new List<ItemVisual>();

        private List<GameObject> InventoryCategories = new List<GameObject>();

        private void Start()
        {
            GameInput.Instance.inputActions.Player.Inventory.performed += EnableInventory;
            GameInput.Instance.inputActions.UI.CloseInventory.performed += EnableInventory;

            InventoryCategories.Add(CardPanel);
            InventoryCategories.Add(ItemPanel);

            OpenInventoryTab(InventoryCategories[0]);

            UIPanelManager.Instance.ClosePanel(InventoryPanel);
        }

        public void InitializeVisual(CardInventory inventory)
        {
            cardInventory = inventory;

            foreach (var card in cardInventory.Cards)
            {
                AddVisual(card);
            }
            foreach (var item in cardInventory.Items)
            {
                AddVisual(item);
            }
        }

        #region Card
        public InventoryCardVisual GetVisual(CardSO card)
        {
            foreach (var visual in CurrentCardVisuals)
            {
                if (visual.card == card)
                    return visual;
            }
            return null;
        }
        public void AddVisual(CardSO card)
        {
            InventoryCardVisual visual = Instantiate(CardVisual.gameObject, CardsVisualInventory).GetComponent<InventoryCardVisual>();
            visual.Initialize(card);
            CurrentCardVisuals.Add(visual);
        }
        public void RemoveVisual(CardSO card)
        {
            InventoryCardVisual visual = GetVisual(card);
            if (visual != null)
            {
                CurrentCardVisuals.Remove(visual);
                Destroy(visual.gameObject);
            }
        }
        #endregion

        #region Item
        public ItemVisual GetVisual(Item item)
        {
            foreach (var visual in CurrentItemVisuals)
            {
                if (visual.Item == item)
                    return visual;
            }
            return null;
        }
        public void AddVisual(Item item)
        {
            ItemVisual visual = Instantiate(ItemVisual.gameObject, ItemsVisualInventory).GetComponent<ItemVisual>();
            visual.Initialize(item);
            CurrentItemVisuals.Add(visual);
        }
        public void RemoveVisual(Item item)
        {
            ItemVisual visual = GetVisual(item);
            if (visual != null)
            {
                CurrentItemVisuals.Remove(visual);
                Destroy(visual.gameObject);
            }
        }
        #endregion

        #region Panels
        public void EnableInventory(InputAction.CallbackContext ctx)
        {
            if (UIPanelManager.Instance.SomethinkIsOpened())
            {
                if (UIPanelManager.Instance.currentOpenedPanel != InventoryPanel)
                {
                    return;
                }
            }

            isOpened = !isOpened;

            if (isOpened)
            {
                UIPanelManager.Instance.OpenPanel(InventoryPanel);
            }
            else
            {
                UIPanelManager.Instance.ClosePanel(InventoryPanel);
            }
        }

        public void OpenInventoryTab(GameObject panel)
        {
            foreach (var item in InventoryCategories)
            {
                item.SetActive(false);
            }

            panel.SetActive(true);
        }
        #endregion

        private void OnDisable()
        {
            GameInput.Instance.inputActions.Player.Inventory.performed -= EnableInventory;
            GameInput.Instance.inputActions.UI.CloseInventory.performed -= EnableInventory;
        }
    }
}
