using UnityEngine;

public class OxygenDamageHandler : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageInterval = 5f;
    public float baseDamage = 5f;
    public float damageIncreasePerTick = 1f;
    public float maxDamage = 20f;

    private float damageTimer;
    private int zeroOxygenTicks;
    private HealthComponent playerHealth;

    private void Update()
    {
        if (OxygenManager.Instance == null)
            return;

        if (playerHealth == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                playerHealth = player.GetComponent<HealthComponent>();

            if (playerHealth == null)
                return;
        }

        if (OxygenManager.Instance.currentOxygen <= 0f)
        {
            damageTimer += Time.deltaTime;

            if (damageTimer >= damageInterval)
            {
                damageTimer = 0f;

                float damage = baseDamage + (zeroOxygenTicks * damageIncreasePerTick);
                damage = Mathf.Min(damage, maxDamage);

                playerHealth.TakeDamage(damage);
                zeroOxygenTicks++;

                if (ScreenDamageFlash.Instance != null)
                    ScreenDamageFlash.Instance.Flash();

                Debug.Log("Oxygen damage: " + damage);
            }
        }
        else
        {
            damageTimer = 0f;
            zeroOxygenTicks = 0;
        }
    }
}