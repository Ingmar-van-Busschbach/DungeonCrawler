using System.Collections.Generic;
using UnityEngine;

namespace Damage
{
    public class StatusEffectContainer : MonoBehaviour
    {
        private List<StatusEntry> statuses = new();
        public void ApplyStatus(StatusInstance statusInstance)
        {
            foreach(EDamageType statusType in statusInstance.statuses)
            {
                statuses.Add(new StatusEntry(statusType, statusInstance.statusDuration, statusInstance.statusDamage));
            }
        }

        private void Update()
        {
            List<StatusEntry> pendingDeletion = new();
            for(int i = 0; i < statuses.Count; i++)
            {
                statuses[i].remainingDuration -= Time.deltaTime;
                if (statuses[i].remainingDuration <= 0)
                {
                    pendingDeletion.Add(statuses[i]);
                }
            }
            foreach(StatusEntry deletedStatusEntry in pendingDeletion)
            {
                statuses.Remove(deletedStatusEntry);
            }
        }
    }
}

