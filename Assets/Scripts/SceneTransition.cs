using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName;
    public string targetSpawnID;
    public bool requireInteraction = false;

    private bool playerInRange = false;

    private void Update()
    {
        if (!requireInteraction) return;

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Transition();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        playerInRange = true;

        if (!requireInteraction)
        {
            Transition();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInRange = false;
    }

    public void Transition()
    {
        if (GameStateManager.Instance != null && GameStateManager.Instance.IsGameplayBlocked())
            return;

        if (PlayerSpawnManager.Instance != null)
        {
            PlayerSpawnManager.Instance.SetPendingSpawn(targetSpawnID);
        }

        SceneManager.LoadScene(targetSceneName);
    }
}