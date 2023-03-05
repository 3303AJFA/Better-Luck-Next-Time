using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.BattleSystem
{
    using Cards;

    public class EnemyOnBattle : MonoBehaviour
    {
        private Image HealthFillAmount;

        public float Damage { 
            get 
            {
                return card.Damage;
            } 
            set 
            {
                card.Damage = value;
            } 
        }
        public float Health { 
            get 
            {
                return card.Health;
            }
            private set 
            {
                card.Health = value;

                HealthFillAmount.fillAmount = card.Health / maxHealth;
            } 
        }
        private float maxHealth;

        private EnemyCardSO card;

        public void Initialize(EnemyCardSO enemyCard, Image healthBar)
        {
            HealthFillAmount = healthBar;
            HealthFillAmount.fillAmount = 1;

            card = Instantiate(enemyCard);
            card.Initialize(this);

            maxHealth = card.Health;
        }

        public void Attack()
        {
            card.Activate();

            StartCoroutine(BattleManager.Instance.ChangeTurn(Turn.Player));
        }

        public void TakeDamage(float dmg)
        {
            Health -= dmg;

            if(Health <= 0)
            {
                BattleManager.Instance.FinishBattle();
            }
        }
    }
}
