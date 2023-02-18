using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        [SerializeField] private AttackCardSO[] AttackCards;
        [field: SerializeField] public EnemyCardSO EnemyCard { get; private set; }

        [Header("UI")]
        [SerializeField] private Transform AttackCardVisual_Parent;
        [SerializeField] private CanvasGroup AttackCardVisual_Parent_CanvasGroup;
        [SerializeField] private CardVisualOnUI AttackCardVisual;

        [Header("Turn")]
        [SerializeField] private Turn m_CurrentTurn;
        [SerializeField] private float TurnChangeTime = 1.2f;

        [Header("Spawn positions")]
        [SerializeField] private Transform EnemySpawnPosition;
        [SerializeField] private Transform PlayerSpawnPosition;

        [Header("Plaer")]
        [SerializeField] private PlayerOnBattle PlayerPrefab;

        public EnemyOnBattle Enemy { get; private set; }
        public PlayerOnBattle Player { get; private set; }
        public Turn CurrentTurn { 
            get
            {
                return m_CurrentTurn;
            }
            set
            {
                m_CurrentTurn = value;

                if(value == Turn.Enemy)
                {
                    Enemy.Attack();

                    AttackCardVisual_Parent_CanvasGroup.alpha = 0.5f;
                }
                else if(value == Turn.Player)
                {
                    AttackCardVisual_Parent_CanvasGroup.alpha = 1f;
                }

                Debug.Log(m_CurrentTurn);
            } 
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            // Spawn cards
            foreach (var card in AttackCards)
            {
                CardVisualOnUI visual = Instantiate(AttackCardVisual, AttackCardVisual_Parent).GetComponent<CardVisualOnUI>();
                visual.Initialize(card);
            }

            // Spawn Enemy
            Enemy = Instantiate(EnemyCard.EnemyPrefab, EnemySpawnPosition.position, Quaternion.identity).GetComponent<EnemyOnBattle>();
            Enemy.Initialize(EnemyCard);

            // Spawn Player
            Player = Instantiate(PlayerPrefab.gameObject, PlayerSpawnPosition.position, Quaternion.identity).GetComponent<PlayerOnBattle>();

            CurrentTurn = Turn.Player;
        }

        public void UseCard(AttackCardSO card)
        {
            if(CurrentTurn == Turn.Player)
            {
                card.Activate();

                StartCoroutine(ChangeTurn(Turn.Enemy, 0));
            }
        }

        public IEnumerator ChangeTurn(Turn turn, float time = 1.2f)
        {
            yield return new WaitForSeconds(time);
            CurrentTurn = turn;
        }

        public void HurtEnemy(float dmg)
        {
            Enemy.TakeDamage(dmg);
        }

        public void FinishBattle()
        {
            Debug.Log("Battle finished");

            SceneManager.LoadScene(0);
        }
    }
}
