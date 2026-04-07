using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float dmg = 10f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 1f;

    private HealthComponent healthComp;
    private HealthComponent playerHealth;
    private Transform playerTransform;

    private bool attacking = false;
    private bool hitProtect = false;

    void Start()
    {
        healthComp = GetComponent<HealthComponent>();
        if (healthComp != null)
            healthComp.OnDeath += () => Destroy(gameObject);

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            playerHealth = player.GetComponent<HealthComponent>();
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        float dist = Vector2.Distance(transform.position, playerTransform.position);
        if (dist <= attackRange && !attacking)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        attacking = true;
        if (playerHealth != null)
            playerHealth.TakeDamage(dmg);
        yield return new WaitForSeconds(attackCooldown);
        attacking = false;
    }

    private IEnumerator HitProtection()
    {
        hitProtect = true;
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.15f);
        hitProtect = false;
    }

    public void TakeDamage(float amount)
    {
        if (hitProtect || healthComp == null) return;
        healthComp.TakeDamage(amount);
        StartCoroutine(HitProtection());
    }
}