using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem
{
    using Cards;

    public class EnemyOnBattle : MonoBehaviour
    {
        public float Damage { get; private set; }
        public float Health { get; private set; }

        private EnemyCardSO card;

        public void Initialize(EnemyCardSO enemyCard)
        {
            card = Instantiate(enemyCard);
            card.Initialize(this);
        }

        public void Attack()
        {
            card.Attack();
        }
    }
}
