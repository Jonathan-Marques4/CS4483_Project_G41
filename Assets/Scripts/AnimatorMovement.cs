using UnityEngine;

public class SimpleTopDownAnimator : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 4f;

    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector2 movement;
    private string currentAnimation;
    private Vector2 lastDirection = Vector2.down;

    void Update(){

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero){

            lastDirection = movement;
        }

        HandleAnimation();
    }

    void FixedUpdate(){

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void HandleAnimation(){

        string targetAnimation;

        if (movement == Vector2.zero){

            if (Mathf.Abs(lastDirection.x) > Mathf.Abs(lastDirection.y)){

                targetAnimation = "idle_side";
                spriteRenderer.flipX = lastDirection.x > 0;
            }

            else if (lastDirection.y > 0){

                targetAnimation = "idle_up";
            }
            
            else{

                targetAnimation = "idle_down";
            }
        }

        else{

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y)){

                targetAnimation = "walk_side";
                spriteRenderer.flipX = movement.x > 0;
            }
            
            else if (movement.y > 0){

                targetAnimation = "walk_up";
            }
            
            else{

                targetAnimation = "walk_down";
            }
        }

        if (currentAnimation != targetAnimation){
            
            animator.Play(targetAnimation);
            currentAnimation = targetAnimation;
        }
    }
}