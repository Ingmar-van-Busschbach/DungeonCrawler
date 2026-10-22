using Quests;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestSO", menuName = "Scriptable Objects/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string questName;
    public string questDisplayText;
    public int questTriggerCountToComplete;

    public Quest CreateQuest()
    {
        return new Quest(questName, questDisplayText, questTriggerCountToComplete);
    }
}
