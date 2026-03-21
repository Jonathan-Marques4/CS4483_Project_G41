using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed = 3f;          // Slightly slower than player usually
    [SerializeField] private float detectionRange = 8f;      // Only chase if player closer than this 
    [SerializeField] private bool faceDirection = true;      // Flip sprite to face player

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Enemy missing Rigidbody2D!", gameObject);
            enabled = false;
            return;
        }

        // Find player once 
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Player' found! Enemy won't move.");
        }
    }

    void FixedUpdate()
    {
        if (player == null) return;

        // Calculate direction to player
        Vector2 toPlayer = (player.position - transform.position);
        float distance = toPlayer.magnitude;

        // Only chase if within range
        if (detectionRange <= 0f || distance <= detectionRange)
        {
            direction = toPlayer.normalized;
            rb.linearVelocity = direction * chaseSpeed;
        }
        else
        {
            // Stop moving if too far 
            rb.linearVelocity = Vector2.zero;
        }

        // Face movement direction 
        if (faceDirection && Mathf.Abs(direction.x) > 0.01f)
        {
            // Assuming sprite faces right by default
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x); // 1 or -1
            transform.localScale = scale;
        }
    }


    void OnDrawGizmosSelected()
    {
        if (detectionRange > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
        }
    }
}