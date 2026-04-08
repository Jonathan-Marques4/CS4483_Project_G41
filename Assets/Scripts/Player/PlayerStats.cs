using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public float maxSprintEnergy;
    public float currentSprintEnergy;
    public float moveSpeed;
    public float sprintSpeedMultiplier;
    public float sprintUsageSpeed;

    public HealthComponent Health { get; private set; }

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        Health = GetComponent<HealthComponent>();
        if (Health == null)
            Debug.LogError("PlayerStats requires a HealthComponent on the same GameObject!", gameObject);
    }
}