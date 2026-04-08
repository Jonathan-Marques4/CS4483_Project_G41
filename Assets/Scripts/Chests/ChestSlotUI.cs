using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ChestSlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;

    [HideInInspector] public int slotIndex;

    public void SetSlot(InventorySlot slot)
    {
        if (slot != null && !slot.IsEmpty())
        {
            iconImage.enabled = true;
            iconImage.sprite = slot.item.icon;
            quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : "";
        }
        else
        {
            iconImage.enabled = false;
            iconImage.sprite = null;
            quantityText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked chest slot " + slotIndex);

        if (ChestTransferController.Instance != null)
        {
            ChestTransferController.Instance.OnChestSlotClicked(slotIndex);
        }
        else
        {
            Debug.LogError("ChestTransferController.Instance is null");
        }
    }
}