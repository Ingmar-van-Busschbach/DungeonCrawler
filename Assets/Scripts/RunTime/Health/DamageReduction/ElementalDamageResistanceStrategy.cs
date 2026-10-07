using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    public class ElementalDamageResistanceStrategy : DamageReductionStrategy
    {
        [SerializeField] private List<ElementalDamageResistanceEntry> elementalDamageResistanceEntries;
        public override float ApplyDamageReduction(DamageEntry damage)
        {
            foreach(ElementalDamageResistanceEntry entry in elementalDamageResistanceEntries)
            {
                if(damage.damageType == entry.damageType)
                {
                    return entry.damageMultiplier;
                }
            }
            return 1.0f;
        }
    }
}

