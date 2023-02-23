using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.BattleSystem.Cards.Attack
{
    [CreateAssetMenu(fileName = "Standart attack card", menuName = "Game/Card/new Standart attack card")]
    public class StandartAttackCard : CardSO
    {
        public override void Activate()
        {
            BattleManager.Instance.HurtEnemy(Damage);
            BattleManager.Instance.Player.Health += HealthIncome;
            BattleManager.Instance.Player.Energy += EnergyIncome;
        }
    }
}
