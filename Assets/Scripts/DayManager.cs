using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Controls;

public class DayManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static DayManager instance;
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private EnergyBar energyBar;

    private int currentDay = 1;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
        updateDayUI();
    }

    public void nextDay()
    {
        currentDay++;
        updateDayUI();

        energyBar.restoreEnergy();
        CropManager.instance.ProcessDayEnd();

    }

    private void updateDayUI()
    {
        if (dayText != null)
        {
            dayText.text = "Day " + currentDay;
        }
    }
}
