using EventBus;
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance;
        public event Action<Quest> onQuestAdded;
        public event Action<Quest> onQuestRemoved;

        public List<QuestSO> startingQuests;
        public List<Quest> currentQuests;

        private void OnEnable()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(startingQuests.Count > instance.startingQuests.Count)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            foreach (QuestSO questSO in startingQuests)
            {
                AddQuest(questSO.CreateQuest());
            }
            InvokeRepeating("Test", 1f, 1f);
        }

        private void Test()
        {
            EventBus<QuestEvent>.Publish(new QuestEvent("KillZombie", 1));
        }

        public void AddQuest(Quest quest)
        {
            EventBus<QuestEvent>.OnEvent += quest.AddProgress;
            quest.onComplete += OnQuestCompleted;
            currentQuests.Add(quest);
            onQuestAdded?.Invoke(quest);
        }

        private void OnQuestCompleted(Quest quest)
        {
            EventBus<QuestEvent>.OnEvent -= quest.AddProgress;
            quest.onComplete -= OnQuestCompleted;
            currentQuests.Remove(quest);
            onQuestRemoved?.Invoke(quest);
        }
    }
}
