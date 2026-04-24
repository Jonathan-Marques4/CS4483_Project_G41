using UnityEngine;

// Attach this to any GameObject that has a HealthComponent.
// Drag the FloatingDamageText prefab into the inspector field.
public class DamageNumberSpawner : MonoBehaviour
{
    [SerializeField] private GameObject floatingTextPrefab;
    [SerializeField] private Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f);

    private HealthComponent healthComp;

    void Awake()
    {
        healthComp = GetComponent<HealthComponent>();
    }

    void OnEnable()
    {
        if (healthComp != null)
            healthComp.OnDamaged += SpawnNumber;
    }

    void OnDisable()
    {
        if (healthComp != null)
            healthComp.OnDamaged -= SpawnNumber;
    }

    private void SpawnNumber(float amount)
    {
        if (floatingTextPrefab == null) return;

        GameObject obj = Instantiate(floatingTextPrefab, transform.position + spawnOffset, Quaternion.identity);
        obj.GetComponent<FloatingDamageText>()?.SetDamage(amount);
    }
}
