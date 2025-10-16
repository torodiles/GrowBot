using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Panels")]
    public GameObject bookUIPanel;
    public GameObject bookButton;
    public GameObject infoUIPanel;
    public GameObject hotbarUIPanel;
    public GameObject questUIPanel;
    public GameObject questCompletePopup;
    public GameObject shopUIPanel;

    [Header("Player Scripts")]
    public PlayerMovement playerMovementScript;

    private bool isBookOpen = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        bookUIPanel.SetActive(false);
        infoUIPanel.SetActive(true);
        hotbarUIPanel.SetActive(true);
        questCompletePopup.SetActive(false);
        shopUIPanel.SetActive(false);
    }
    public void HideGameUI()
    {
        infoUIPanel.SetActive(false);
        hotbarUIPanel.SetActive(false);
        questUIPanel.SetActive(false);
        bookButton.SetActive(false);

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }
    }

    public void ShowGameUI()
    {
        infoUIPanel.SetActive(true);
        hotbarUIPanel.SetActive(true);
        questUIPanel.SetActive(true);
        bookButton.SetActive(true);

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }
    }

    public void ToggleBookUI()
    {
        isBookOpen = !isBookOpen;
        bookUIPanel.SetActive(isBookOpen);

        if (isBookOpen)
        {
            HideGameUI();
            bookButton.SetActive(true);
        }
        else
        {
            ShowGameUI();
        }
    }

    public void ShowQuestCompletePopup(Quest completedQuest)
    {
        HideGameUI();

        questUIPanel.SetActive(true);
        questCompletePopup.SetActive(true);

        questCompletePopup.GetComponent<QuestCompletedPopup>().Setup(completedQuest);
    }
}
