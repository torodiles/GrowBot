using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public Button buyButton;

    Item item;
    int price;
    ShopManager manager;

    public void Setup(Item item, int price, ShopManager manager)
    {
        this.item = item;
        this.price = price;
        this.manager = manager;

        icon.sprite = item.icon;
        nameText.text = item.itemName;
        priceText.text = price.ToString();

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => manager.OnBuyRequested(this, 1)); // qty 1, bisa diganti
    }

    public Item GetItem() => item;
    public int GetPrice() => price;
}