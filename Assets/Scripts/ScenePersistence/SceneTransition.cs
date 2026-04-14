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

        SaveCurrentSceneState();

        if (PlayerSpawnManager.Instance != null)
        {
            PlayerSpawnManager.Instance.SetPendingSpawn(targetSpawnID);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene(targetSceneName);
    }

    private void SaveCurrentSceneState()
    {
        ChestStateHandler[] chests = FindObjectsByType<ChestStateHandler>(FindObjectsSortMode.None);
        foreach (ChestStateHandler chest in chests)
        {
            chest.SaveState();
        }

        FarmTileStateHandler[] farmTiles = FindObjectsByType<FarmTileStateHandler>(FindObjectsSortMode.None);
        foreach (FarmTileStateHandler tile in farmTiles)
        {
            tile.SaveState();
        }
    }
}