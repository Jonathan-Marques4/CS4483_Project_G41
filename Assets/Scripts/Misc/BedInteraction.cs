using UnityEngine;

public class BedInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public int sleepHours = 8;

    public void Sleep()
    {
        
        if (PlayerEnergy.Instance != null)
        {
            PlayerEnergy.Instance.RestoreFullEnergy();
        }

        
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.AdvanceHours(sleepHours);
        }

        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            HealthComponent health = player.GetComponent<HealthComponent>();

            if (health != null)
            {
                health.Heal(25f); 
            }
        }

        Debug.Log("Player slept, restored energy and gained 25 HP.");
    }

    public bool IsPlayerInRange(Transform playerTransform)
    {
        if (playerTransform == null) return false;

        return Vector2.Distance(playerTransform.position, transform.position) <= interactRange;
    }
}