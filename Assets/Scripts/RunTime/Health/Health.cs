using UnityEngine;
using System.Collections.Generic;
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

        public List<DamageReductionStrategy> damageReductionStrategies = new();

        public event Action<float, float> OnHealthChanged;
        public event Action<float, int> OnDamageDisplay;
        public event Action OnDeath;
        
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
        /// Apply Damage function which should be called when attempting to change the health by a certain damage amount of a certain damage type. Calls the "ApplyDamageReduction" function in the DamageReductionStrategy, which can handle damage reduction.
        /// </summary>
        public float ApplyDamage(DamageInstance damage)
        {
            float totalDamage = 0;
            foreach(DamageEntry damageEntry in damage.damageEntries)
            {
                float damageMultiplierRatio = 1;
                foreach (DamageReductionStrategy damageReductionStrategy in damageReductionStrategies)
                {
                    damageMultiplierRatio *= damageReductionStrategy.ApplyDamageReduction(damageEntry);
                }
                totalDamage += damageEntry.damage.Value * damageMultiplierRatio;
            }
            CurrentHealth -= totalDamage;
            OnDamageDisplay?.Invoke(totalDamage, damage.criticalTier);
            return totalDamage;
        }
    }
}
