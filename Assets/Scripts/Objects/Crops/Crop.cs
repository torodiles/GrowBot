using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Crop", menuName = "Scriptable Objects/Crop")]
public class Crop : ScriptableObject
{
    public string cropName;
    public int daysToGrow;
    public List<TileBase> growStageTiles;
    //public int sellValue;
    public Item harvestableItem;
}
