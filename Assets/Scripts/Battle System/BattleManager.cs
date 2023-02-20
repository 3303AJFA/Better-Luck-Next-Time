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

        [Header("Cards")]
        [SerializeField] private AttackCardSO[] AllCards;
        [SerializeField] private int MaxCardAmount = 3;
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
                    if(CurrentCards.Count <= 0 | CurrentCards == null)
                    {
                        FinishBattle();
                    }

                    AttackCardVisual_Parent_CanvasGroup.alpha = 1f;
                }

                Debug.Log(m_CurrentTurn + " Turn!");
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
                AttackCardSO selectedCard = AllCards[Random.Range(0, AllCards.Length)];
                CurrentCards.Add(selectedCard);
            }

            // Spawn cards
            foreach (var card in CurrentCards)
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
                CurrentCards.Remove(card);

                Player.Energy += card.GivenEnergy;
                if(Player.Energy >= Player.MaxEnergy)
                {
                    int chance = Random.Range(0, 2); // return 0 or 1
                    Debug.Log(chance);
                    if(chance == 1)
                    {
                        HurtPlayer(card.TakenHealth);
                    }
                }
                else
                {
                    HurtPlayer(card.TakenHealth);
                }

                StartCoroutine(ChangeTurn(Turn.Enemy, TurnChangeTime));
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
        public void HurtPlayer(float dmg)
        {
            Player.TakeDamage(dmg);
        }

        public void FinishBattle()
        {
            Debug.Log("Battle finished");

            SceneManager.LoadScene(0);
        }
    }
}
