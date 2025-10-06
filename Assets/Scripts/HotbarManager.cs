using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarManager : MonoBehaviour
{
    public Slot[] slots;
    private int selectedIndex = 0;

    void Start()
    {
        SelectSlot(0);
    }

    void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
                SelectSlot(i);
        }
    }

    public void SelectSlot(int index)
    {
        if (index < 0 || index >= slots.Length) return;

        selectedIndex = index;
    }

    public Slot GetSelectedSlot()
    {
        return slots[selectedIndex];
    }

    public ToolType GetCurrentTool()
    {
        return slots[selectedIndex].GetToolType();
    }

    public void AddItem(Item item, int amount = 1)
    {
        foreach (var slot in slots)
        {
            if (slot.CanStack(item))
            {
                int space = item.maxStack - slot.quantity;
                int toAdd = Mathf.Min(space, amount);
                slot.AddItem(toAdd);
                amount -= toAdd;
                if (amount <= 0) return;
            }
        }

        foreach (var slot in slots)
        {
            if (slot.currentItem == null)
            {
                int toAdd = Mathf.Min(item.maxStack, amount);
                slot.SetItem(item, toAdd);
                amount -= toAdd;
                if (amount <= 0) return;
            }
        }

    }
}
