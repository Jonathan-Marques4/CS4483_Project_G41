using UnityEngine;

public class CropItemDebugTester : MonoBehaviour
{
    public ItemData strawberrySeeds;
    public ItemData strawberry;
    public ItemData turnipSeeds;
    public ItemData turnip;
    public ItemData potatoSeeds;
    public ItemData potato;
    public ItemData onionSeeds;
    public ItemData onion;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("F1 pressed");

            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager.Instance is NULL");
                return;
            }

            if (strawberrySeeds == null)
            {
                Debug.LogError("strawberrySeeds is NOT assigned");
                return;
            }

            //bool success = InventoryManager.Instance.AddItem(strawberrySeeds, 5);
            //Debug.Log("Tried adding Strawberry Seeds, success = " + success);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("F2 pressed");

            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager.Instance is NULL");
                return;
            }

            if (strawberry == null)
            {
                Debug.LogError("strawberry is NOT assigned");
                return;
            }

            //bool success = InventoryManager.Instance.AddItem(strawberry, 3);
            //Debug.Log("Tried adding Strawberry, success = " + success);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Debug.Log("F3 pressed");

            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager.Instance is NULL");
                return;
            }

            if (potatoSeeds == null)
            {
                Debug.LogError("potatoSeeds is NOT assigned");
                return;
            }

            //bool success = InventoryManager.Instance.AddItem(potatoSeeds, 5);
            //Debug.Log("Tried adding Potato Seeds, success = " + success);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            Debug.Log("F4 pressed");

            if (InventoryManager.Instance == null)
            {
                Debug.LogError("InventoryManager.Instance is NULL");
                return;
            }

            if (potato == null)
            {
                Debug.LogError("potato is NOT assigned");
                return;
            }

            //bool success = InventoryManager.Instance.AddItem(potato, 3);
            //Debug.Log("Tried adding Potato, success = " + success);
        }
    }
}