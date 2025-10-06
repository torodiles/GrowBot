using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Slot : MonoBehaviour
{
    public Item currentItem;
    public int quantity = 1;

    private Image iconImage;
    private TextMeshProUGUI quantityText;

    void Awake()
    {
        iconImage = GetComponent<Image>();
        quantityText = GetComponentInChildren<TextMeshProUGUI>();
        if (quantityText != null)
            quantityText.enabled = false;
    }

    void Start()
    {
        UpdateSlotUI();
    }

    public void SetItem(Item item, int amount = 1)
    {
        currentItem = item;
        quantity = amount;
        UpdateSlotUI();
    }

    public void AddItem(int amount)
    {
        if (currentItem == null) return;
        quantity += amount;
        UpdateSlotUI();
    }

    public void RemoveItem(int amount = 1)
    {
        if (currentItem == null) return;

        quantity -= amount;
        if (quantity <= 0)
        {
            ClearSlot();
        }
        else
        {
            UpdateSlotUI();
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        quantity = 0;
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        if (iconImage == null) return;

        if (currentItem != null)
        {
            iconImage.sprite = currentItem.icon;
            iconImage.enabled = true;

            if (quantityText != null)
            {
                if (currentItem.isStackable && quantity >= 1)
                {
                    quantityText.text = quantity.ToString();
                    quantityText.enabled = true;
                }
                else
                {
                    quantityText.enabled = false;
                }
            }
        }
        else
        {
            iconImage.sprite = null;
            iconImage.enabled = false;
            if (quantityText != null) quantityText.enabled = false;
        }
    }

    public bool CanStack(Item newItem)
    {
        return currentItem != null && currentItem == newItem && currentItem.isStackable && quantity < currentItem.maxStack;
    }

    public ToolType GetToolType()
    {
        return currentItem != null ? currentItem.toolType : ToolType.None;
    }

}
