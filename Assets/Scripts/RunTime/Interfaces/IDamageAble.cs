using Damage;

namespace Health
{
    public interface IDamageAble
    {
        public abstract float ApplyDamage(DamageInstance damage);
    }
}
