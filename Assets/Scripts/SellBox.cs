using System.Collections.Generic;
using UnityEngine;

public class SellBox : MonoBehaviour
{
    public static SellBox instance;

    [Header("Sprites")]
    [SerializeField] private Sprite openSprite;
    [SerializeField] private Sprite closedSprite;

    [Header("UI")]
    [SerializeField] private GameObject closeBoxPopup;

    private SpriteRenderer spriteRenderer;
    private List<Item> itemsToSell = new List<Item>();
    private bool isPlayerInRange = false;
    private bool isOpen = true;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        OpenForNewDay();
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (isOpen)
            {
                closeBoxPopup.SetActive(true);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TryAddItem();
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    CloseBox();
                }
            }
            else
            {
                closeBoxPopup.SetActive(false);
            }
        }
        else
        {
            closeBoxPopup.SetActive(false);
        }
    }

    private void TryAddItem()
    {
        Slot selectedSlot = HotbarManager.instance.GetSelectedSlot();
        if (selectedSlot != null && selectedSlot.currentItem != null)
        {
            Item itemToAdd = selectedSlot.currentItem;

            if (itemToAdd.sellPrice > 0 && itemToAdd.toolType == ToolType.None)
            {
                itemsToSell.Add(itemToAdd);
                selectedSlot.RemoveItem(1);
                Debug.Log("Menambahkan " + itemToAdd.itemName + " ke dalam sell box. Total item: " + itemsToSell.Count);
            }
        }
    }

    private void CloseBox()
    {
        isOpen = false;
        spriteRenderer.sprite = closedSprite;
        closeBoxPopup.SetActive(false);
        Debug.Log("Sell box ditutup. Siap untuk dikirim.");
    }

    public void ProcessSale()
    {
        int totalValue = 0;
        foreach (Item item in itemsToSell)
        {
            totalValue += item.sellPrice;
        }

        if (totalValue > 0)
        {
            MoneyManager.instance.AddMoney(totalValue);
            Debug.Log("Penjualan berhasil! Mendapatkan " + totalValue + "G.");
        }

        itemsToSell.Clear();
    }

    public void OpenForNewDay()
    {
        isOpen = true;
        spriteRenderer.sprite = openSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
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
