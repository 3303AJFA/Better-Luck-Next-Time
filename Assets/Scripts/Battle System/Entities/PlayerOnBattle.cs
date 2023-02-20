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
        [SerializeField] private Image EnergyFillAmount;

        [Header("Health")]
        private float m_Health;
        [SerializeField] private float MaxHealth;

        [Header("Energy")]
        private float m_Energy;
        public float MaxEnergy;

        public float Health
        {
            get
            {
                return m_Health;
            }
            private set
            {
                m_Health = value;

                HealthFillAmount.fillAmount = m_Health / MaxHealth;
            }
        }
        public float Energy {
            get 
            {
                return m_Energy;
            }
            set
            {
                m_Energy = value;

                if(m_Energy >= MaxEnergy)
                {
                    m_Energy = MaxEnergy;
                }

                EnergyFillAmount.fillAmount = m_Energy / MaxEnergy;
            }
        }

        private void Start()
        {
            Health = MaxHealth;
            Energy = 0;
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
