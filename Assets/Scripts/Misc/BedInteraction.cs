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

        Debug.Log("Player slept for " + sleepHours + " hours and restored full energy.");
    }

    public bool IsPlayerInRange(Transform playerTransform)
    {
        if (playerTransform == null) return false;

        return Vector2.Distance(playerTransform.position, transform.position) <= interactRange;
    }
}