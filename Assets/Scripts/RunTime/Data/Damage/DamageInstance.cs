using Damage;
using System.Collections.Generic;
public struct DamageInstance
{
    public List<DamageEntry> damageEntries;
    public int criticalTier;

    public DamageInstance(int criticalTier)
    {
        damageEntries = new();
        this.criticalTier = criticalTier;
    }
}
