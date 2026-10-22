using EventBus;
using System;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class Quest
    {
        public string questName;
        public string questDisplayText;
        [HideInInspector] public int questCurrentTriggerCount;
        public int questTriggerCountToComplete;
        [HideInInspector] public bool isComplete;

        public event Action<Quest> onComplete;
        public event Action<Quest> onProgessChanged;

        public Quest(string questName, string questDisplayText, int countToComplete)
        {
            this.questName = questName;
            this.questDisplayText = questDisplayText;
            questTriggerCountToComplete = countToComplete;
        }

        private void CheckCompletion()
        {
            if(questCurrentTriggerCount >= questTriggerCountToComplete)
            {
                isComplete = true;
                onComplete?.Invoke(this);
            }
        }

        public void AddProgress(QuestEvent questEvent)
        {
            if(questEvent.questName != questName)
            {
                return;
            }
            if (isComplete)
            {
                return;
            }

            questCurrentTriggerCount += questEvent.value;
            if(questCurrentTriggerCount > questTriggerCountToComplete)
            {
                questCurrentTriggerCount = questTriggerCountToComplete;
            }
            onProgessChanged?.Invoke(this);
            CheckCompletion();
        }
    }
}