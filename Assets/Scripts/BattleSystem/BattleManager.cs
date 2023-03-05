using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using NaughtyAttributes;

namespace Game.BattleSystem
{
    using Cards;
    using UIVisual;
    using Utilities;
    using Player.Inventory;

    public enum Turn
    {
        Player,
        Enemy
    }

    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [Header("Cards")]
        [SerializeField, Expandable] private CardsListSO CardsList;
        [SerializeField] private int MaxCardAmount = 3;
        [field: SerializeField] public EnemyCardSO EnemyCard { get; private set; }

        [Header("Turn")]
        [SerializeField, ReadOnly] private Turn m_CurrentTurn;
        [SerializeField] private float TurnChangeTime = 1.2f;

        [Header("Enemy positions")]
        [SerializeField] private Transform EnemySpawnPosition;
        [SerializeField] private Image EnemyHealthBar;

        [Header("Player")]
        public PlayerOnBattle Player;
        [ReadOnly] public List<CardSO> CurrentCards = new List<CardSO>();

        // UI VISUAL
        [Foldout("Card visual"), SerializeField] private Transform AttackCardVisual_Parent;
        [Foldout("Card visual"), SerializeField] private CanvasGroup AttackCardVisual_Parent_CanvasGroup;
        [Foldout("Card visual"), SerializeField] private CardVisual AttackCardVisual;

        [Foldout("Turn visual"), SerializeField] private TextMeshProUGUI CurrentTurnText;
        [Foldout("Turn visual"), SerializeField] private Button SkipTurnButton;

        [Foldout("Card inventories visual"), SerializeField] private TextMeshProUGUI CardsInInventoryAmountText;
        [Foldout("Card inventories visual"), SerializeField] private TextMeshProUGUI UsedCardsAmountText;

        public EnemyOnBattle Enemy { get; private set; }
        public Turn CurrentTurn { 
            get
            {
                return m_CurrentTurn;
            }
            private set
            {
                m_CurrentTurn = value;
                CurrentTurnText.text = $"{m_CurrentTurn} Turn!";

                if (value == Turn.Enemy)
                {
                    Enemy.Attack();

                    AttackCardVisual_Parent_CanvasGroup.alpha = 0.5f;
                    SkipTurnButton.interactable = false;
                }
                else if(value == Turn.Player)
                {
                    if(CurrentCards.Count <= 0 | CurrentCards == null)
                    {
                        RestoreCardsFromInventory();
                    }

                    AttackCardVisual_Parent_CanvasGroup.alpha = 1f;
                    SkipTurnButton.interactable = true;
                }
            } 
        }

        private string savePath;
        private CardInventory cardInventory;
        private List<CardSO> usedCards = new List<CardSO>();

        private void Awake()
        {
            savePath = $"{Application.dataPath}/";

            Instance = this;

            Load();
        }

        private void Start()
        {
            // Select card in round
            for (int i = 0; i < MaxCardAmount; i++)
            {
                CardSO selectedCard = cardInventory.GetRandomCard();
                if(selectedCard != null)
                {
                    cardInventory.RemoveCard(selectedCard);
                    CurrentCards.Add(selectedCard);
                }
                else
                {
                    break;
                }
            }

            // Spawn cards
            foreach (var card in CurrentCards)
            {
                SpawnCard(card);
            }

            // Spawn Enemy
            Enemy = Instantiate(EnemyCard.EnemyPrefab, EnemySpawnPosition.position, Quaternion.identity).GetComponent<EnemyOnBattle>();
            Enemy.Initialize(EnemyCard, EnemyHealthBar);

            // Starting Player
            CurrentTurn = Turn.Player;

            UpdateVisual();
        }

        public void UseCard(CardSO card)
        {
            card.Activate();
            CurrentCards.Remove(card);

            cardInventory.RemoveCard(card);
            usedCards.Add(card);

            UpdateVisual();

            StartCoroutine(ChangeTurn(Turn.Enemy, TurnChangeTime));
        }
        public void SkipPlayerTurn()
        {
            if(CurrentTurn == Turn.Player)
            {
                StartCoroutine(ChangeTurn(Turn.Enemy, 0));
            }
        }
        public void RestoreCardsFromInventory()
        {
            for (int i = 0; i < MaxCardAmount; i++)
            {
                if(cardInventory.CardsAmount == 0)
                {
                    foreach (var usedCard in usedCards)
                    {
                        cardInventory.AddCard(usedCard);
                    }
                    usedCards.Clear();
                }

                CardSO selectedCard = cardInventory.GetRandomCard();
                if(selectedCard != null)
                {
                    cardInventory.RemoveCard(selectedCard);
                    CurrentCards.Add(selectedCard);
                    SpawnCard(selectedCard);
                }
                else
                {
                    break;
                }
            }

            UpdateVisual();
        }

        public void HurtEnemy(float dmg)
        {
            Enemy.TakeDamage(dmg);
        }
        public void HurtPlayer(float dmg)
        {
            Player.Health -= dmg;
        }

        public void FinishBattle()
        {
            Debug.Log("Battle finished");

            if(CurrentCards.Count > 0)
            {
                foreach (var card in CurrentCards)
                {
                    cardInventory.AddCard(card);
                }
            }

            Save();

            SceneManager.LoadScene(0);
        }

        #region Utilities
        private void UpdateVisual()
        {
            CardsInInventoryAmountText.text = cardInventory.CardsAmount.ToString();
            UsedCardsAmountText.text = usedCards.Count.ToString();
        }

        public IEnumerator ChangeTurn(Turn turn, float time = 1.2f)
        {
            yield return new WaitForSeconds(time);
            CurrentTurn = turn;
        }
        private void SpawnCard(CardSO cardData)
        {
            CardVisual visual = Instantiate(AttackCardVisual, AttackCardVisual_Parent).GetComponent<CardVisual>();
            visual.Initialize(cardData);
        }

        private void Save()
        {
            SaveDataUtility.SaveData(cardInventory, SaveDataUtility.CARD_INVENTORY_FILENAME, savePath);
        }
        private void Load()
        {
            cardInventory = (CardInventory)SaveDataUtility.LoadData<CardInventory>(savePath, SaveDataUtility.CARD_INVENTORY_FILENAME, new CardInventory(CardsList.AllAttackCards));
        }
        #endregion
    }
}
