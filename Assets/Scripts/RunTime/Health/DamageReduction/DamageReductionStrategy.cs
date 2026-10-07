using UnityEngine;

namespace Damage
{
    public abstract class DamageReductionStrategy : MonoBehaviour
    {
        public abstract float ApplyDamageReduction(DamageEntry damage);
    }
}

