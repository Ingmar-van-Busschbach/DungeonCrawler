using Buffables;

namespace Damage
{
    [System.Serializable]
    public struct DamageEntry
    {
        public BuffableFloat damage;
        public EDamageType damageType;

        public DamageEntry(BuffableFloat damage, EDamageType damageType)
        {
            this.damage = damage;
            this.damageType = damageType;
        }
    }
}
