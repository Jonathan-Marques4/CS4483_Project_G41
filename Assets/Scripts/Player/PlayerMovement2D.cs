using UnityEngine;

public class PlayerMovement2D : MonoBehaviour{

    
    public float moveSpeed = 4f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector2 movement;
    public Vector2 LastMoveDirection { get; private set; } = Vector2.down;
    private Vector2 lastMoveDirection = Vector2.down;

    void Update(){

        if (GameStateManager.Instance != null && GameStateManager.Instance.IsGameplayBlocked())
        {
            movement = Vector2.zero;
            HandleAnimation();
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero){

            lastMoveDirection = movement;
            LastMoveDirection = movement;
        }

        HandleAnimation();

        if (movement.x > 0.01f){

            spriteRenderer.flipX = true;
        }

        else if (movement.x < -0.01f){

            spriteRenderer.flipX = false;
        }
    }

    void FixedUpdate(){

        if (GameStateManager.Instance != null && GameStateManager.Instance.IsGameplayBlocked())
            return;

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleAnimation(){
        
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);
    }
}