using TMPro;
using UnityEngine;
using EventBus;
using System.Collections.Generic;

namespace Quests
{
    public class QuestDisplaySubscriber : MonoBehaviour
    {
        [SerializeField] private Dictionary<string, TMP_Text> questTexts;
        [SerializeField] private Vector2 textDisplaySize;
        [SerializeField] private int fontSize = 24;
        [SerializeField] private float deletionDelay = 2f;

        public void OnEnable()
        {
            QuestManager.instance.onQuestAdded += OnQuestAdded;
        }

        public void OnDisable()
        {
            QuestManager.instance.onQuestAdded -= OnQuestAdded;
        }

        private void OnQuestAdded(Quest questData)
        {
            GameObject textObject = Instantiate(new GameObject("Quest " + questTexts.Count + ": " + questData.questName), transform);
            RectTransform rect = textObject.AddComponent<RectTransform>();
            rect.sizeDelta = textDisplaySize;
            TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
            text.fontSize = fontSize;
            text.text = string.Format(questData.questDisplayText, 0, questData.questTriggerCountToComplete);
            questData.onProgessChanged += OnQuestProgressChanged;
            questData.onComplete += OnQuestCompleted;
            questTexts.Add(questData.questName, text);
        }

        private void OnQuestProgressChanged(Quest questData)
        {
            if (questTexts.ContainsKey(questData.questName))
            {
                questTexts[questData.questName].text = string.Format(questData.questDisplayText, questData.questCurrentTriggerCount, questData.questTriggerCountToComplete);
            }
        }

        private void OnQuestCompleted(Quest questData)
        {
            questData.onProgessChanged -= OnQuestProgressChanged;
            questData.onComplete -= OnQuestCompleted;
            if (questTexts.ContainsKey(questData.questName))
            {
                questTexts[questData.questName].text = "Quest completed!";
                Destroy(questTexts[questData.questName].gameObject, deletionDelay);
                questTexts.Remove(questData.questName);
            }
        }
    }

}