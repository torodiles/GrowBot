using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestCompletedPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questTitleText;

    [Header("Item Reward UI")]
    [SerializeField] private GameObject itemRewardGroup;
    [SerializeField] private Image itemRewardImage;
    [SerializeField] private TextMeshProUGUI itemRewardAmountText;
    [SerializeField] private Button itemRewardButton;
    [SerializeField] private Sprite coinRewardImage;

    private Quest completedQuest;

    public void Setup(Quest quest)
    {
        completedQuest = quest;
        questTitleText.text = "\"" + quest.title + "\"";

        if (completedQuest.itemReward != null && completedQuest.itemRewardAmount > 0)
        {
            itemRewardGroup.SetActive(true);

            itemRewardImage.sprite = completedQuest.itemReward.icon;
            itemRewardAmountText.text = completedQuest.itemRewardAmount.ToString();

        }
        else
        {
            itemRewardGroup.SetActive(true);
            // kalo no reward / ga ada item
           
            itemRewardImage.sprite = coinRewardImage;
            itemRewardAmountText.text = completedQuest.moneyReward.ToString();

            // nambah coin or 0
        }
    }

    public void OnClaimButtonPressed()
    {
        if (completedQuest == null) return;

        if (completedQuest.itemReward != null && completedQuest.itemRewardAmount > 0)
        {
            HotbarManager.instance.AddItem(completedQuest.itemReward, completedQuest.itemRewardAmount);
        }

        if (completedQuest.moneyReward > 0)
        {
            MoneyManager.instance.AddMoney(completedQuest.moneyReward);
        }

        //Time.timeScale = 1f;

        UIManager.instance.ShowGameUI();

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
