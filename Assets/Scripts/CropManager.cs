using NUnit.Framework;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class CropData
{
    public int growStage;
    //public float growTimer;

    public CropData()
    {
        growStage = 0;
        //growTimer = 0f;
    }
}
// untuk sekarang, growth berdasarkan second, tetapi bisa dijadikan tumbuh per hari.
public class CropManager : MonoBehaviour
{
    public static CropManager instance;

    [SerializeField] private Tilemap cropTilemap;
    //[SerializeField] private float timeNeeded = 10f;
    [SerializeField] private int cropSellValue = 15;
    [SerializeField] private List<TileBase> growStageTiles;
    private Dictionary<Vector3Int, CropData> growingCrops;

    [SerializeField] private Tilemap plantableTilemap;
    [SerializeField] private TileBase wateredSoilTile;
    [SerializeField] private TileBase soilTile;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        growingCrops = new Dictionary<Vector3Int, CropData>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    foreach (var cropPosition in new List<Vector3Int>(growingCrops.Keys))
    //    {
    //        CropData crop = growingCrops[cropPosition];

    //        crop.growTimer += Time.deltaTime;

    //        if (crop.growTimer >= timeNeeded)
    //        {
    //            crop.growTimer = 0f;
    //            crop.growStage++;

    //            if (crop.growStage >= growStageTiles.Count)
    //            {
    //                growingCrops.Remove(cropPosition);
    //            }
    //            else
    //            {
    //                UpdateCropTile(cropPosition);
    //            }
    //        }
    //    }


    //}
    public void ProcessDayEnd()
    {
        foreach(var cropPosition in new List<Vector3Int>(growingCrops.Keys))
        {
            CropData crop = growingCrops[cropPosition];

            TileBase groundTile = plantableTilemap.GetTile(cropPosition);

            if (groundTile == wateredSoilTile)
            {
                if (crop.growStage < growStageTiles.Count - 1)
                {
                    crop.growStage++;
                    UpdateCropTile(cropPosition);
                }

                plantableTilemap.SetTile(cropPosition, soilTile);
            }

        }
    }

    public void PlantCrop(Vector3Int position)
    {
        if (growingCrops.ContainsKey(position)) return;

        growingCrops.Add(position, new CropData());
        UpdateCropTile(position);

    }

    private void UpdateCropTile(Vector3Int position)
    {
        if (growingCrops.TryGetValue(position, out CropData crop))
        {
            cropTilemap.SetTile(position, growStageTiles[crop.growStage]);
        }
    }

    public bool isHarvestable(Vector3Int position)
    {
        if (growingCrops.TryGetValue(position, out CropData crop))
        {
            if (crop.growStage >= growStageTiles.Count - 1)
            {
                return true;
            }
        }
        return false;
    }

    public void HarvestCrop(Vector3Int position)
    {
        if (isHarvestable(position))
        {
            cropTilemap.SetTile(position, null);
            growingCrops.Remove(position);

            MoneyManager.instance.AddMoney(cropSellValue);
        }
    }
}
