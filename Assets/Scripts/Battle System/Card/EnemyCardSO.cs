using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem.Cards
{
    public abstract class EnemyCardSO : ScriptableObject
    {
        public GameObject EnemyPrefab;
        [Space]
        public float Damage;
        public float Health;

        protected EnemyOnBattle enemy;

        public virtual void Initialize(EnemyOnBattle enemyOnBattle)
        {
            enemy = enemyOnBattle;
        }

        public virtual void Attack() { }
    }
}
