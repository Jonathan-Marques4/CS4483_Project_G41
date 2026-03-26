using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private float dmg;
    private bool attacking;
    private float healthMax;
    private float health;

    private float moveSpeed = 5f;

    private bool hitProtect = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attacking = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        healthMax = 50f;
        health = healthMax;
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 playerDir = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = playerDir * moveSpeed ;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!attacking)
            {
                Debug.Log("Attacking Player");
                StartCoroutine(attack());
            }
        }
    }

    private IEnumerator attack()
    {
        attacking = true;
        PlayerStats.Instance.takeDmg(dmg);
        yield return new WaitForSeconds(1f);
        attacking = false;
    }

    private IEnumerator hitProtection()
    {
        hitProtect = true;
        moveSpeed = 0;
        yield return new WaitForSeconds(0.15f);
        moveSpeed = 5;
        hitProtect = false;
    }

    public void takeDmg(float dmg)
    {
        if (!hitProtect)
        {
            health -= dmg;
            Debug.Log("Health:" + health + "/" + healthMax);
            StartCoroutine(hitProtection());
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
