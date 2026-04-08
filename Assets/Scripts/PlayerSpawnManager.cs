using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;

    private string pendingSpawnID;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SetPendingSpawn(string spawnID)
    {
        pendingSpawnID = spawnID;
        Debug.Log("Pending spawn set to: " + pendingSpawnID);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        Debug.Log("Looking for spawn ID: " + pendingSpawnID);

        if (string.IsNullOrEmpty(pendingSpawnID))
            return;

        SceneSpawnPoint[] spawnPoints = FindObjectsByType<SceneSpawnPoint>(FindObjectsSortMode.None);

        foreach (SceneSpawnPoint spawnPoint in spawnPoints)
        {
            Debug.Log("Found spawn point: " + spawnPoint.spawnID + " at " + spawnPoint.transform.position);

            if (spawnPoint.spawnID == pendingSpawnID)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawnPoint.transform.position;
                    Debug.Log("Moved player to spawn point: " + spawnPoint.spawnID);
                }
                else
                {
                    Debug.LogError("Player not found by tag.");
                }

                pendingSpawnID = null;
                return;
            }
        }

        Debug.LogWarning("No matching spawn point found for spawn ID: " + pendingSpawnID);
        pendingSpawnID = null;
    }
}