using UnityEngine;
using System;
using Buffables;
using Damage;

namespace Health
{
    public class Health : MonoBehaviour, IDamageAble
    {
        [SerializeField]
        private BuffableFloat maxHealth;
        [SerializeField]
        private float currentHealth;

        public DamageReductionStrategy damageReductionStrategy;

        public Action<float, float> OnHealthChanged;
        public Action<float, int> OnHealthDisplay;
        public Action OnDeath;
        
        public float CurrentHealth
        {
            set
            {
                currentHealth = value;
                if(currentHealth <= 0)
                {
                    OnDeath?.Invoke();
                }
                if(currentHealth > maxHealth.Value)
                {
                    currentHealth = maxHealth.Value;
                }
                OnHealthChanged?.Invoke(currentHealth, value);
            }
            get
            {
                return currentHealth;
            }
        }

        /// <summary>
        /// Apply Damage function which should be called when attempting to change the health by a certain damage amount of a certain damage type. Calls the "ApplyDamageReduction function, which can handle damage reduction on child classes."
        /// </summary>
        public float ApplyDamage(DamageInstance damage)
        {
            float totalDamage = 0;
            foreach(DamageEntry damageEntry in damage.damageEntries)
            {
                totalDamage += damageReductionStrategy.ApplyDamageReduction(damageEntry);
            }
            CurrentHealth -= totalDamage;
            OnHealthDisplay?.Invoke(totalDamage, damage.criticalTier);
            return totalDamage;
        }
    }
}
