using UnityEngine;

public class PlayerMovement2D : MonoBehaviour{

    public float moveSpeed = 4f;
    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector2 movement;
    private Vector2 lastMoveDirection = Vector2.down;

    void Update(){

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero){

            lastMoveDirection = movement;
        }

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        animator.SetFloat("LastMoveX", lastMoveDirection.x);
        animator.SetFloat("LastMoveY", lastMoveDirection.y);

        if (movement.x > 0.01f){

            spriteRenderer.flipX = true;
        }

        else if (movement.x < -0.01f){

            spriteRenderer.flipX = false;
            
        }
    }

    void FixedUpdate(){
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}