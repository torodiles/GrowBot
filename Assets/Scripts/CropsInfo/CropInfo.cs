using UnityEngine;

[CreateAssetMenu(fileName = "CropInfo", menuName = "Scriptable Objects/CropInfo")]
public class CropInfo : ScriptableObject
{
    [Header("Info")]
    public string plantName;
    public string scientificName;
    public Sprite plantSprite;
    public string plantMoney; // harvest price
    public string growthTime;

    [Header("Description")]
    [TextArea(5, 10)]
    public string plantDescription;
}
