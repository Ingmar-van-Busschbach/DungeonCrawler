namespace Damage
{
    public class StatusEntry
    {
        public EDamageType statusType;
        public float remainingDuration;
        public readonly float statusDamage;

        public StatusEntry(EDamageType statusType, float remainingDuration, float statusDamage)
        {
            this.statusType = statusType;
            this.remainingDuration = remainingDuration;
            this.statusDamage = statusDamage;
        }
    }
}