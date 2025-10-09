using UnityEngine;

public enum GoalType
{
    Gather,
    GoTo,
    PlantSeed
}

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requiredAmount;
    public string requiredItemName;
}

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable Objects/Quest")]
public class Quest : ScriptableObject
{
    public string title;
    [TextArea(3, 5)]
    public string description;
    public QuestGoal goal;

    [Header("Rewards")]
    public int moneyReward;
    public Item itemReward;
    public int itemRewardAmount;
}
