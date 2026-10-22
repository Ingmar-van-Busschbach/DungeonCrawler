using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Damage
{
    public class ElementalDamageResistanceStrategy : DamageReductionStrategy
    {
        [SerializeField] private Dictionary<EDamageType, float> elementalDamageResistanceEntries;
        public override float ApplyDamageReduction(DamageEntry damage)
        {
            if (elementalDamageResistanceEntries.ContainsKey(damage.damageType))
            {
                return elementalDamageResistanceEntries[damage.damageType];
            }
            return 1.0f;
        }
    }
}

