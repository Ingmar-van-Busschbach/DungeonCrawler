using UnityEngine;
using System;

namespace Health
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth;
        [SerializeField]
        private float currentHealth;

        public Action<float, float> OnHealthChanged;
        public Action OnDeath;
        
        public float CurrentHealth
        {
            set
            {
                currentHealth = value;
                if(currentHealth < 0)
                {
                    OnDeath?.Invoke();
                }
                if(currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
                OnHealthChanged?.Invoke(currentHealth, value);
            }
            get
            {
                return currentHealth;
            }
        }
    }
}
