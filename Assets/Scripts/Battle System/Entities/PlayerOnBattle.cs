using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.BattleSystem
{
    public class PlayerOnBattle : MonoBehaviour
    {
        [Header("Visual")]
        [SerializeField] private Image HealthFillAmount;

        [Header("Health")]
        private float m_Health;
        [SerializeField] private float m_MaxHealth;

        public float Health
        {
            get
            {
                return m_Health;
            }
            private set
            {
                m_Health = value;

                HealthFillAmount.fillAmount = m_Health / m_MaxHealth;
            }
        }

        private void Start()
        {
            Health = m_MaxHealth;
        }

        public void TakeDamage(float dmg)
        {
            Health -= dmg;

            if(Health <= 0)
            {
                Debug.Log("Player loose");
                BattleManager.Instance.FinishBattle();
            }
        }
    }
}
