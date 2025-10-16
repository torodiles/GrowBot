using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 8f;

    [Header("External References")]
    [SerializeField] private PlayerControls playerControls;  // Pastikan ini diatur di Inspector
    [SerializeField] private HotbarManager hotbarManager;

    // Komponen Internal
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.gravityScale = 0;
    }

    void Update()
    {
        ProcessInputs();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }

    private void ProcessInputs()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(horizontal, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleAction();
        }
    }

    private void UpdateAnimation()
    {
        Slot selectedSlot = hotbarManager.GetSelectedSlot();
        int currentToolId = 0;

        if (selectedSlot != null && selectedSlot.currentItem != null)
        {
            switch (selectedSlot.GetToolType())
            {
                case ToolType.WaterCan: currentToolId = 1; break;
                case ToolType.Hoe: currentToolId = 2; break;
            }
        }
        animator.SetFloat("HeldToolType", (float)currentToolId);

        float movementSpeed = Mathf.Abs(moveInput.x);
        animator.SetFloat("Speed", movementSpeed);

        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0;
        }
    }

    private void HandleAction()
    {
        if (playerControls == null)
        {
            Debug.LogError("Referensi PlayerControls belum diatur di Inspector!");
            return;
        }

        Slot selectedSlot = hotbarManager.GetSelectedSlot();
        if (selectedSlot == null || selectedSlot.currentItem == null) return;

        ToolType tool = selectedSlot.GetToolType();

        switch (tool)
        {
            case ToolType.Hoe:
                playerControls.TryTill();
                animator.SetTrigger("useTool");
                break;
            case ToolType.WaterCan:
                playerControls.TryWater();
                animator.SetTrigger("useTool");
                break;
            case ToolType.Seed:
                playerControls.TryPlant();
                break;
            case ToolType.Basket:
                playerControls.TryHarvest();
                break;
        }
    }
}