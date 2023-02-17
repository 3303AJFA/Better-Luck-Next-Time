using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem
{
    using Cards;

    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [SerializeField] private AttackCardSO[] AttackCards;
        [field: SerializeField] public EnemyCardSO EnemyCard { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {

        }
    }
}
