using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Chase Settings")]
    [SerializeField] private float chaseSpeed = 7f;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private bool faceDirection = true;

    private Rigidbody2D rb;
    private Transform player;
    private Vector2 direction;
    private bool touchingPlayer = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) player = playerObj.transform;
    }

    void FixedUpdate()
    {
        if (player == null) return;

        Vector2 toPlayer = (player.position - transform.position);
        float distance = toPlayer.magnitude;

        if (touchingPlayer)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (detectionRange <= 0f || distance <= detectionRange)
        {
            direction = toPlayer.normalized;
            rb.linearVelocity = direction * chaseSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }

        if (faceDirection && Mathf.Abs(direction.x) > 0.01f)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Sign(direction.x);
            transform.localScale = scale;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            touchingPlayer = true;
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
            touchingPlayer = false;
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