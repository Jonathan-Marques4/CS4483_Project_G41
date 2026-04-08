using UnityEngine;
using System.Collections;
public class Knife : EquippableItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject hitBox;
    private bool attacking = false;
    private float dmg = 5f;
    private float swingRecoverySpeed = 0.4f;

    void Start()
    {
        if (hitBox != null)
        {
            hitBox.SetActive(false);
        }
    }
    public override void useItem()
    {
        attack();
    }

    private void attack()
    {
        if (!attacking)
        {
            StartCoroutine(attackSequence());
        }
        Debug.Log("Attacking");
    }

    private IEnumerator attackSequence()
    {
        if (hitBox != null && !attacking)
        {
            attacking = true;
            hitBox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            hitBox.SetActive(false);
            yield return new WaitForSeconds(swingRecoverySpeed);
            attacking = false;
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy e = other.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(dmg);
            }
        }
    }
}
