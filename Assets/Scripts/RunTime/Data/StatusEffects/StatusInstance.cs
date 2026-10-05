using System.Collections.Generic;
using Buffables;

namespace Damage
{
    public struct StatusInstance
    {
        public List<EDamageType> statuses;
        public readonly float statusDamage;
        public readonly float statusDuration;

        public StatusInstance(float statusDamage, float statusDuration)
        {
            statuses = new();
            this.statusDamage = statusDamage;
            this.statusDuration = statusDuration;
        }
    }
}