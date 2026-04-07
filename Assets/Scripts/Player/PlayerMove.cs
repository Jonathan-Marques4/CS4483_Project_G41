using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 1.8f;

    private float SprintSpeed => walkSpeed * sprintMultiplier;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isSprinting;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError($"No Rigidbody2D found on {gameObject.name}!", this);
            enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 input = moveInput.sqrMagnitude > 0.01f ? moveInput.normalized : Vector2.zero;

        float currentSpeed = isSprinting ? SprintSpeed : walkSpeed;

        rb.linearVelocity = input * currentSpeed;

        Debug.Log($"Vel: {rb.linearVelocity:F2} | Input: {moveInput:F2} | Sprint: {isSprinting}");
    }


    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void Sprint(InputAction.CallbackContext context)
    {
        isSprinting = context.ReadValueAsButton();

    }


    private void OnDisable()
    {
        moveInput = Vector2.zero;
        isSprinting = false;
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        if (rb == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, rb.linearVelocity * 0.5f);
    }
}