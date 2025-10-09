using System.Collections.Generic;
using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    [Header("Quest Data")]
    [SerializeField] private List<Quest> availableQuests;

    [Header("Visuals")]
    [SerializeField] private GameObject questAvailableIndicator;

    private bool isPlayerInRange = false;
    private int dayQuestWasLastTaken = -1;
    private int currentQuestIndex = 0;

    void Start()
    {
        CheckQuestAvailability();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            TryAcceptQuest();
        }
    }

    private void TryAcceptQuest()
    {
        if (QuestManager.instance.activeQuest != null)
        {
            Debug.Log("Selesaikan dulu quest yang sedang aktif!");
            return;
        }

        if (DayManager.instance.currentDay <= dayQuestWasLastTaken)
        {
            Debug.Log("Tidak ada quest baru hari ini. Cek lagi besok!");
            return;
        }

        if (currentQuestIndex >= availableQuests.Count)
        {
            Debug.Log("Semua quest sudah selesai!");
            return;
        }

        Quest questToGive = availableQuests[currentQuestIndex];
        QuestManager.instance.StartQuest(questToGive);
        Debug.Log("Quest baru diterima: " + questToGive.title);

        dayQuestWasLastTaken = DayManager.instance.currentDay;
        currentQuestIndex++;

        CheckQuestAvailability();
    }

    private void CheckQuestAvailability()
    {
        bool canTakeQuest = QuestManager.instance.activeQuest == null &&
                            DayManager.instance.currentDay > dayQuestWasLastTaken &&
                            currentQuestIndex < availableQuests.Count;

        if (questAvailableIndicator != null)
        {
            questAvailableIndicator.SetActive(canTakeQuest);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            CheckQuestAvailability();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
