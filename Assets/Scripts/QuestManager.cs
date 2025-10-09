using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    [Header("Quest Info")]
    public Quest activeQuest;

    [Header("Player Progress")]
    private int currentAmount = 0;

    [Header("References")]
    public QuestUI questUI;
    public GameObject questCompletedPopup;

    [Header("Popup System")]
    [SerializeField] private GameObject questCompletedPopupPrefab;
    [SerializeField] private Transform canvasTransform;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        //questCompletedPopup.SetActive(false);
    }

    private void Start()
    {
        //questUI.SetInitialState();
    }
    public void StartQuest(Quest questToStart)
    {
        if (activeQuest != null)
        {
            //Debug.LogWarning("Sudah ada quest yang aktif!");
            return;
        }

        activeQuest = questToStart;
        currentAmount = 0;

        questUI.UpdateQuestUI(activeQuest, currentAmount);
    }

    public void AdvanceQuest(string itemName, int amount = 1)
    {
        if (activeQuest != null && activeQuest.goal.goalType == GoalType.Gather && activeQuest.goal.requiredItemName == itemName)
        {
            currentAmount += amount;
            CheckCompletion();

            questUI.UpdateQuestUI(activeQuest, currentAmount);
        }
    }

    public void AdvancePlantingQuest(string seedItemName)
    {
        if (activeQuest != null && activeQuest.goal.goalType == GoalType.PlantSeed && activeQuest.goal.requiredItemName == seedItemName)
        {
            currentAmount++;
            CheckCompletion();

            questUI.UpdateQuestUI(activeQuest, currentAmount);
        }
    }
    private void CheckCompletion()
    {
        if (currentAmount >= activeQuest.goal.requiredAmount)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        //Debug.Log("Quest '" + activeQuest.title + "' Selesai!");

        //GameObject popupInstance = Instantiate(questCompletedPopupPrefab, canvasTransform);
        //popupInstance.SetActive(true);

        UIManager.instance.ShowQuestCompletePopup(activeQuest);

        //popupInstance.GetComponent<QuestCompletedPopup>().Setup(activeQuest);

        activeQuest = null;
        currentAmount = 0;
    }
}
