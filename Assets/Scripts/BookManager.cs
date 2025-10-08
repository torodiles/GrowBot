using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{
    [Header("Crop Data List")]
    public List<CropInfo> allCrops;

    [Header("UI References")]
    public TextMeshProUGUI plantNameText;
    public TextMeshProUGUI scientificNameText;
    public Image plantImage;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI growthTimeText;
    public TextMeshProUGUI descriptionText;

    private int currentPageIndex = 0;

    void OnEnable()
    {
        if (allCrops != null && allCrops.Count > 0)
        {
            DisplayCrop(currentPageIndex);
        }
        else
        {
            Debug.LogWarning("Tidak ada data tanaman di Book!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            NextPage();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            PreviousPage();
        }
    }

    public void NextPage()
    {
        currentPageIndex++;
        if (currentPageIndex >= allCrops.Count)
        {
            currentPageIndex = 0;
        }
        DisplayCrop(currentPageIndex);
    }

    public void PreviousPage()
    {
        currentPageIndex--;
        if (currentPageIndex < 0)
        {
            currentPageIndex = allCrops.Count - 1;
        }
        DisplayCrop(currentPageIndex);
    }

    private void DisplayCrop(int index)
    {
        CropInfo currentCrop = allCrops[index];

        plantNameText.text = currentCrop.plantName;
        scientificNameText.text = currentCrop.scientificName;
        plantImage.sprite = currentCrop.plantSprite;
        moneyText.text = currentCrop.plantMoney;
        growthTimeText.text = currentCrop.growthTime;
        descriptionText.text = currentCrop.plantDescription;
    }
}
