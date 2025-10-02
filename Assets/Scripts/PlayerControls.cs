using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap plantableTilemap;
    [SerializeField] private Tilemap cropTilemap;

    [SerializeField] private TileBase dirtTile;
    [SerializeField] private TileBase soilTile;
    [SerializeField] private TileBase cropStage1Tile;
    [SerializeField] private TileBase wateredSoilTile;

    private Vector2Int facingDirection = Vector2Int.down;

    [SerializeField] private EnergyBar energyBar;
    private bool nearBed = false;
    void Update()
    {   
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 moveInput = new Vector2(horizontal, vertical);
        if (moveInput != Vector2.zero)
        {
            facingDirection = new Vector2Int(Mathf.RoundToInt(moveInput.x), Mathf.RoundToInt(moveInput.y));
        }

        // plant dengan Space
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (energyBar.hasEnergy())
            {
                TryInteract();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && nearBed)
        {
            DayManager.instance.nextDay();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryWater();
        }
    }

    private void TryWater()
    {
        if (!energyBar.hasEnergy())
        {
            return;
        }

        Vector3Int targetCell = groundTilemap.WorldToCell(transform.position) + new Vector3Int(facingDirection.x, facingDirection.y, 0);

        TileBase tile = plantableTilemap.GetTile(targetCell);

        if (tile == soilTile)
        {
            plantableTilemap.SetTile(targetCell, wateredSoilTile);
            energyBar.reduceEnergy();
        }
    }

// TryInteract -> bisa plant, water (belom), dan harvest
    private void TryInteract()
    {
        Vector3Int currentCell = groundTilemap.WorldToCell(transform.position);
        Vector3Int targetCell = currentCell + new Vector3Int(facingDirection.x, facingDirection.y, 0);

        if (CropManager.instance.isHarvestable(targetCell))
        {
            energyBar.reduceEnergy();
            CropManager.instance.HarvestCrop(targetCell);
            return;
        }


        TileBase plantableTile = plantableTilemap.GetTile(targetCell);
        if (plantableTile == null) return;

        if (plantableTile.name.Contains("Dirt"))
        {
            plantableTilemap.SetTile(targetCell, soilTile);
            Debug.Log("Tilled land at: " + targetCell);
            energyBar.reduceEnergy();
            return;
        }

        if (plantableTile.name.Contains("Soil"))
        {
            if (!cropTilemap.HasTile(targetCell))
            {
                CropManager.instance.PlantCrop(targetCell);
                Debug.Log("Planted crop at: " + targetCell);
                energyBar.reduceEnergy();
            }
        }

        //Debug.Log("Target tile: " + plantableTile.name);
        //Debug.Log("Crop at: " + targetCell);
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
