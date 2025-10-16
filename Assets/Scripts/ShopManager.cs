using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [System.Serializable]
    public class ShopEntry
    {
        public Item item;
        public int price = 10;
        //public int stock = -1; // -1 = infinite
    }

    public ShopEntry[] shopEntries;
    public GameObject shopItemPrefab; // ShopItemPrefab
    public Transform contentParent;   // content under scrollrect
    public GameObject shopPanel;
    public TMP_Text moneyText;
    public HotbarManager hotbarManager;
    public UIManager UImanager;
    public GameObject shopUIPanel;

    private List<ShopItemUI> spawned = new List<ShopItemUI>();

    void Start()
    {
        PopulateShop();
        UpdateMoneyDisplay();
    }

    public void PopulateShop()
    {
        foreach (Transform t in contentParent) Destroy(t.gameObject);
        spawned.Clear();

        foreach (var entry in shopEntries)
        {
            GameObject go = Instantiate(shopItemPrefab, contentParent);
            ShopItemUI ui = go.GetComponent<ShopItemUI>();
            ui.Setup(entry.item, entry.price, this);
            spawned.Add(ui);
        }
    }

    public void OnBuyRequested(ShopItemUI itemUI, int qty)
    {
        int price = itemUI.GetPrice() * qty;
        // check money
        if (!MoneyManager.instance.Spend(price))
        {
            // show not enough money feedback
            Debug.Log("Not enough money");
            // TODO: popup or sound
            return;
        }

        // find shop entry to reduce stock
        //var entry = System.Array.Find(shopEntries, e => e.item == itemUI.GetItem());
        //if (entry != null && entry.stock > 0)
        //{
        //    entry.stock -= qty;
        //}

        // add to hotbar/inventory
        hotbarManager.AddItem(itemUI.GetItem(), qty);

        // update UI stock display
        UpdateMoneyDisplay();

        // optional: feedback sound/effect
        Debug.Log($"Bought {itemUI.GetItem().itemName} x{qty} for {price}");
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0f; // pause game optionally
        UpdateMoneyDisplay();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1f;
        UImanager.ShowGameUI();
        shopUIPanel.SetActive(false);
    }

    public void UpdateMoneyDisplay()
    {
        if (moneyText != null)
            moneyText.text = "" + MoneyManager.instance.money;
    }
}
