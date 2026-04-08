using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IPointerClickHandler
{
    public Image iconImage;
    public TextMeshProUGUI quantityText;
    public GameObject selectionHighlight;

    [HideInInspector] public int slotIndex;
    [HideInInspector] public InventoryClickController clickController;

    public void SetSlot(InventorySlot slot, bool selected = false)
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

        if (selectionHighlight != null)
        {
            selectionHighlight.SetActive(selected);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickController != null)
        {
            clickController.OnSlotClicked(slotIndex);
        }
    }
}