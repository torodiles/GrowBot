using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI progressText;

    public void SetInitialState()
    {
        gameObject.SetActive(true);
        titleText.text = "> You have no quests right now..";
        descriptionText.text = "";
        progressText.text = "";
    }
    public void UpdateQuestUI(Quest quest, int currentAmount)
    {
        gameObject.SetActive(true);

        if (quest == null)
        {
            titleText.text = "> You have no quests right now..";
            descriptionText.text = "";
            progressText.text = "";
        }
        else
        {
            titleText.text = "> " + quest.title;
            descriptionText.text = quest.description;

            if (quest.goal.goalType == GoalType.Gather || quest.goal.goalType == GoalType.PlantSeed)
            {
                progressText.text = string.Format("{0}:\n[{1} / {2}]", quest.goal.requiredItemName, currentAmount, quest.goal.requiredAmount);
            }
            else
            {
                progressText.text = "";
            }
        }

    }
}
