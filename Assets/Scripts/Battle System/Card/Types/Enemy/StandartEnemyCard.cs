using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem.Cards.Enemy
{
    [CreateAssetMenu(fileName = "Standart enemy card", menuName = "Game/Card/new Standart enemy card")]
    public class StandartEnemyCard : EnemyCardSO
    {
        public override void Initialize(EnemyOnBattle enemyOnBattle)
        {
            base.Initialize(enemyOnBattle);
        }
        public override void Activate()
        {
            BattleManager.Instance.Player.TakeDamage(Damage);
        }
    }
}
