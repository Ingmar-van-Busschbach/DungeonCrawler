using Damage;
using UnityEngine;

public class DamageReductionStrategy : MonoBehaviour
{
    public virtual float ApplyDamageReduction(DamageEntry damage)
    {
        return damage.damage.Value;
    }
}
