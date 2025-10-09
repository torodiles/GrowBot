using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControls : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap plantableTilemap;
    [SerializeField] private Tilemap cropTilemap;

    [Header("Tiles")]
    [SerializeField] private TileBase dirtTile;
    [SerializeField] private TileBase soilTile;
    [SerializeField] private TileBase wateredSoilTile;

    [Header("References")]
    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private HotbarManager hotbarManager;

    private Vector2Int facingDirection = Vector2Int.down;
    private bool nearBed = false;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 moveInput = new Vector2(horizontal, vertical);
        if (moveInput != Vector2.zero)
            facingDirection = new Vector2Int(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y));

        Slot selectedSlot = hotbarManager.GetSelectedSlot();
        if (selectedSlot == null || selectedSlot.currentItem == null) return;

        ToolType tool = selectedSlot.GetToolType();

        if (Input.GetKeyDown(KeyCode.Space) && energyBar.hasEnergy())
        {
            switch (tool)
            {
                case ToolType.Hoe: TryTill(); break;
                case ToolType.WaterCan: TryWater(); break;
                case ToolType.Seed: TryPlant(); break;
                case ToolType.Basket: TryHarvest(); break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && nearBed)
            DayManager.instance.nextDay();
    }

    private void TryTill()
    {
        Vector3Int target = GetFacingCell();
        TileBase tile = plantableTilemap.GetTile(target);
        if (tile != null && tile.name.Contains("Dirt"))
        {
            plantableTilemap.SetTile(target, soilTile);
            energyBar.reduceEnergy();
        }
    }

    private void TryPlant()
    {
        Vector3Int target = GetFacingCell();
        TileBase tile = plantableTilemap.GetTile(target);

        Slot selectedSlot = hotbarManager.GetSelectedSlot();
        Item seedItem = selectedSlot.currentItem;

        if (tile != null && tile.name.Contains("Soil") && !cropTilemap.HasTile(target) && seedItem.cropToGrow != null)
        {
            CropManager.instance.PlantCrop(target, seedItem.cropToGrow);

            QuestManager.instance.AdvancePlantingQuest(seedItem.name); // quest

            energyBar.reduceEnergy();

            if (seedItem.isStackable)
            {
                selectedSlot.RemoveItem(1);
            }
        }
    }

    private void TryHarvest()
    {
        Vector3Int target = GetFacingCell();
        if (CropManager.instance.isHarvestable(target))
        {
            CropManager.instance.HarvestCrop(target);
            energyBar.reduceEnergy();
        }
    }

    private void TryWater()
    {
        Vector3Int target = GetFacingCell();
        TileBase tile = plantableTilemap.GetTile(target);
        if (tile != null && tile == soilTile)
        {
            plantableTilemap.SetTile(target, wateredSoilTile);
            energyBar.reduceEnergy();
        }
    }

    private Vector3Int GetFacingCell()
    {
        Vector3Int currentCell = groundTilemap.WorldToCell(transform.position);
        return currentCell + new Vector3Int(facingDirection.x, facingDirection.y, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bed"))
        {
            nearBed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bed"))
        {
            nearBed = false;
        }
    }
}
