using UnityEngine;

namespace Damage
{
    [System.Serializable]
    public struct ElementalDamageResistanceEntry
    {
        public EDamageType damageType;
        public float damageMultiplier;

        public ElementalDamageResistanceEntry(EDamageType damageType, float damageMultiplier)
        {
            this.damageType = damageType;
            this.damageMultiplier = damageMultiplier;
        }
    }
}