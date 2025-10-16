using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private int startingMoney = 50;

    public int money;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            money = startingMoney;
            UpdateMoneyUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetMoney()
    {
        return money;
    }

    public void AddMoney(int amount)
    {
        if (amount <= 0) return;
        money += amount;
        UpdateMoneyUI();
    }

    public bool Spend(int amount)
    {
        if (amount <= 0) return true;
        if (money >= amount)
        {
            money -= amount;
            UpdateMoneyUI();
            return true;
        }
        return false;
    }

    public void SetMoney(int value)
    {
        money = value;
        UpdateMoneyUI();
    }

    private void UpdateMoneyUI()
    {
        if (moneyText != null)
            moneyText.text = money.ToString();
    }
}
