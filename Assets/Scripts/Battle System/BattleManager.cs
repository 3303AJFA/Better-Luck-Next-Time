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

        [Header("UI")]
        [SerializeField] private Transform AttackCardVisual_Parent;
        [SerializeField] private CanvasGroup AttackCardVisual_Parent_CanvasGroup;
        [SerializeField] private CardVisualOnUI AttackCardVisual;
        [Space]
        [SerializeField] private TextMeshProUGUI CurrentTurnText;

        [Header("Turn")]
        [SerializeField] private Turn m_CurrentTurn;
        [SerializeField] private float TurnChangeTime = 1.2f;

        [Header("Enemy positions")]
        [SerializeField] private Transform EnemySpawnPosition;
        [SerializeField] private Image EnemyHealthBar;

        [field: Header("Player")]
        [field: SerializeField] public PlayerOnBattle Player { get; private set; }

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
                }
                else if(value == Turn.Player)
                {
                    if(CurrentCards.Count <= 0 | CurrentCards == null)
                    {
                        FinishBattle();
                    }

                    AttackCardVisual_Parent_CanvasGroup.alpha = 1f;
                }

            } 
        }

        [HideInInspector] public List<AttackCardSO> CurrentCards = new List<AttackCardSO>();

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Select card in round
            for (int i = 0; i < MaxCardAmount; i++)
            {
                AttackCardSO selectedCard = CardsList.AllAttackCards[Random.Range(0, CardsList.AllAttackCards.Length)];
                CurrentCards.Add(selectedCard);
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
        }

        public void UseCard(AttackCardSO card)
        {
            card.Activate();
            CurrentCards.Remove(card);

            StartCoroutine(ChangeTurn(Turn.Enemy, TurnChangeTime));
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

            SceneManager.LoadScene(0);
        }

        #region Utilities
        public IEnumerator ChangeTurn(Turn turn, float time = 1.2f)
        {
            yield return new WaitForSeconds(time);
            CurrentTurn = turn;
        }
        private void SpawnCard(AttackCardSO cardData)
        {
            CardVisualOnUI visual = Instantiate(AttackCardVisual, AttackCardVisual_Parent).GetComponent<CardVisualOnUI>();
            visual.Initialize(cardData);
        }
        #endregion
    }
}
