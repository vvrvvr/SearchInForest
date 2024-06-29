using UnityEngine;

public class Quest : MonoBehaviour
{
    private QuestManager questManager;
    
    public void SetQuestManager(QuestManager manager)
    {
        questManager = manager;
    }

    public void ConditionComplete()
    {
        questManager.CompleteQuest(1);
    }
}
