using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void UpdateMovementAnimation(Vector2 moveInput)
    {
        float movementSpeed = Mathf.Abs(moveInput.x);
        animator.SetFloat("Speed", movementSpeed);

        if (moveInput.x != 0)
        {
            spriteRenderer.flipX = moveInput.x < 0; // true jika ke kiri, false jika ke kanan
        }
    }

    // alat yang dipegang (0=Kosong, 1=WaterCan, 2=Hoe)
    public void UpdateHoldingAnimation(int toolId)
    {
        animator.SetInteger("HeldToolType", toolId);
    }

    public void PlayToolActionAnimation()
    {
        animator.SetTrigger("useTool");
    }
}
