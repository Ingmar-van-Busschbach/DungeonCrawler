using UnityEngine;

namespace EventBus
{
    [System.Serializable]
    public class QuestEvent : Event
    {
        public readonly string questName;
        public readonly int value;
        public QuestEvent(string questName, int value)
        {
            this.questName = questName;
            this.value = value;
        }
    }
}
