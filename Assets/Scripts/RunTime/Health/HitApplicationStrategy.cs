namespace Damage
{
    public class HitApplicationStrategy
    {
        private static void ApplyHit(DamageStruct damageStruct, Health.Health health, StatusEffectContainer statusEffectContainer)
        {
            DamageInstance damageInstance = damageStruct.CriticalDamage();
            health.ApplyDamage(damageInstance);

            StatusInstance statusInstance = damageStruct.StatusEffects();
            statusEffectContainer.ApplyStatus(statusInstance);
        }
    }
}

