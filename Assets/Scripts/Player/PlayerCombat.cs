using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float baseDmg = 15f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackCooldown = 0.4f;

    private bool canAttack = true;
    private Camera cam;
    private Animator animator;
    private SimpleTopDownAnimator topDownAnimator;
    private WeaponVisualController weaponVisual;

    void Start()
    {
        cam = Camera.main;
        animator = GetComponentInChildren<Animator>();
        topDownAnimator = GetComponent<SimpleTopDownAnimator>();
        weaponVisual = GetComponent<WeaponVisualController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        canAttack = false;

        // Direction toward mouse (used for hitbox)
        Vector2 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mouseWorld - (Vector2)transform.position).normalized;

        Vector2 facing = topDownAnimator != null ? topDownAnimator.LastDirection : Vector2.down;

        // Use equipped weapon damage if a weapon is selected, otherwise base damage
        float dmg = baseDmg;
        bool hasSword = false;
        if (InventoryManager.Instance != null)
        {
            var slot = InventoryManager.Instance.GetSelectedHotbarSlot();
            if (slot != null && !slot.IsEmpty() && slot.item != null && slot.item.itemType == ItemType.Weapon)
            {
                dmg = slot.item.weaponDamage;
                hasSword = true;
            }
        }

        // Sword equipped: sword sweep only. No sword: play body hit animation.
        if (hasSword)
        {
            weaponVisual?.PlaySwing(facing);
        }
        else if (animator != null)
        {
            int attackDir;
            if (Mathf.Abs(facing.y) > Mathf.Abs(facing.x))
                attackDir = facing.y > 0 ? 1 : 2;
            else
                attackDir = 0;

            animator.SetInteger("AttackDir", attackDir);
            animator.SetTrigger("Attack");
        }

        // Hit all enemies in range toward mouse
        Vector2 hitCenter = (Vector2)transform.position + dir * (attackRange * 0.6f);
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
