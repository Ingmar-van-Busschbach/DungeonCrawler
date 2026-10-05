using UnityEngine;
using System.Collections.Generic;
using Buffables;
using Damage;

namespace Damage
{
    [System.Serializable]
    public struct DamageStruct
    {
        public BuffableFloat baseDamageMultiplier;
        public List<DamageEntry> elementalDamage;
        public BuffableFloat criticalChance;
        public BuffableFloat criticalDamage;
        public BuffableFloat statusChance;
        public BuffableFloat statusDamage;
        public BuffableFloat statusDuration;


        public DamageInstance CriticalDamage()
        {
            float criticalTier = Mathf.Floor(criticalChance.Value / 100);
            float remainingCriticalChance = criticalChance.Value % 100;
            if (Random.value * 100 <= remainingCriticalChance)
            {
                criticalTier++;
            }
            float criticalDamage = this.criticalDamage.Value * criticalTier;
            DamageInstance damageInstance = new DamageInstance((int)criticalTier);
            foreach (DamageEntry damageEntry in elementalDamage)
            {
                damageInstance.damageEntries.Add(new DamageEntry(new BuffableFloat(damageEntry.damage.Value * criticalDamage * baseDamageMultiplier.Value), damageEntry.damageType));
            }
            
            return damageInstance;
        }

        public StatusInstance StatusEffects()
        {
            StatusInstance statusInstance = new StatusInstance(statusDamage.Value, statusDuration.Value);
            float statusTier = Mathf.Floor(statusChance.Value / 100);
            float remainingStatusChance = statusChance.Value % 100;
            if(Random.value * 100 <= remainingStatusChance)
            {
                statusTier++;
            }
            float totalDamage = 0;
            foreach (DamageEntry damageEntry in elementalDamage)
            {
                totalDamage += damageEntry.damage.Value;
            }
            for(int i = 0; i < statusTier; i++)
            {
                float randomValue = Random.value * totalDamage;
                float accumulativeDamage = 0;
                foreach(DamageEntry damageEntry in elementalDamage)
                {
                    accumulativeDamage += damageEntry.damage.Value;
                    if(randomValue<= accumulativeDamage)
                    {
                        statusInstance.statuses.Add(damageEntry.damageType);
                        break;
                    }
                }
            }
            return statusInstance;
        }
    }
}

