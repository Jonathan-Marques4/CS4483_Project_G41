using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject equippedItem;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact();
        }

        if (Input.GetMouseButtonDown(0))
        {
            useItem();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (equippedItem != null)
            {
                setEquipped(null);
            }
        }
    }

    void interact()
    {
        
    }

    void useItem()
    {
        if (equippedItem != null)
        {
            EquippableItem eItem =  equippedItem.GetComponent<EquippableItem>();
            if (eItem != null)
            {
                eItem.useItem();
            }
        }
    }

    public void setEquipped(GameObject item)
    {
        equippedItem.transform.SetParent(null);
        equippedItem = item;
        equippedItem.transform.SetParent(gameObject.transform);
    }

    public GameObject getEquipped()
    {
        return equippedItem;
    }
}
