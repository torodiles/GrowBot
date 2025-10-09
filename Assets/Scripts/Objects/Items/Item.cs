using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ToolType toolType;
    public bool isStackable;
    public int maxStack = 64;
    public int sellPrice;

    public Crop cropToGrow;
}
