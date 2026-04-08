using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float dmg = 15f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 0.4f;

    private bool canAttack = true;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        canAttack = false;

        // Find direction toward mouse
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouseWorld - (Vector2)transform.position).normalized;

        // Offset the hit center slightly in the attack direction
        Vector2 hitCenter = (Vector2)transform.position + dir * (attackRange * 0.6f);

        // Hit all enemies in range
        Collider2D[] hits = Physics2D.OverlapCircleAll(hitCenter, attackRange);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                var enemy = hit.GetComponent<Enemy>();
                if (enemy != null)
                    enemy.TakeDamage(dmg);
            }
        }

        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}