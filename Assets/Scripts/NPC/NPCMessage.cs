using UnityEngine;
using TMPro;

public class NPCMessage : MonoBehaviour
{
    public GameObject messagePanel;
    public TextMeshProUGUI messageText;
    [TextArea] public string npcMessage;

    private bool playerInRange = false;

    void Start()
    {
        messagePanel.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && Input.GetMouseButtonDown(0))
        {
            messageText.text = npcMessage;
            messagePanel.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            messagePanel.SetActive(false);
        }
    }
}