using UnityEngine;

public class ChestInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    public ChestInventory chestInventory;

    public bool IsPlayerInRange(Transform playerTransform)
    {
        if (playerTransform == null) return false;
        return Vector2.Distance(playerTransform.position, transform.position) <= interactRange;
    }

    public void OpenChest()
    {
        if (ChestUI.Instance != null)
        {
            ChestUI.Instance.OpenChest(this);
        }
    }
}