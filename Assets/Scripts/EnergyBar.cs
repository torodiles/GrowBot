using System;
using UnityEngine;
using UnityEngine.UI;
public class EnergyBar : MonoBehaviour
{
    [SerializeField] int energyDecrease;
    public Text valueText;
    [SerializeField] int energy;
    [SerializeField] int maxEnergy;
    public Slider slider;
    private void Start()
    {
        slider.maxValue = energy;
        slider.value = energy;
    }
    public void OnSliderChanged(float value)
    {
        valueText.text = value.ToString();
    }

    public bool hasEnergy()
    {
        return energy > 0;
    }
    public void reduceEnergy()
    {
        if (energy > 0)
        {
            energy = energy - energyDecrease;
            slider.value = energy;
            Debug.Log(energy);
        }
    }

    public void restoreEnergy()
    {
        energy = maxEnergy;
        slider.value = maxEnergy;
    }
}
