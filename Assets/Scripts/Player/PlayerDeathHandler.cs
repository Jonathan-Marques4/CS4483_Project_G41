using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    public string respawnSceneName = "SpawnArea";
    public string respawnSpawnID = "Default";

    private HealthComponent health;
    private bool isDead = false;

    private void Start()
    {
        health = GetComponent<HealthComponent>();

        if (health != null)
            health.OnDeath += HandleDeath;
    }

    private void OnDestroy()
    {
        if (health != null)
            health.OnDeath -= HandleDeath;
    }

    private void HandleDeath()
    {
        if (isDead) return;
        isDead = true;

        StartCoroutine(DeathRoutine());
    }

    private IEnumerator DeathRoutine()
    {
        ResetInventory();

        if (DeathScreenUI.Instance != null)
            yield return StartCoroutine(DeathScreenUI.Instance.ShowDeathScreen());
        else
            yield return new WaitForSeconds(5f);

        if (PlayerEnergy.Instance != null)
            PlayerEnergy.Instance.RestoreFullEnergy();

        if (OxygenManager.Instance != null)
            OxygenManager.Instance.currentOxygen = OxygenManager.Instance.maxOxygen;

        if (PlayerSpawnManager.Instance != null)
            PlayerSpawnManager.Instance.SetPendingSpawn(respawnSpawnID);

        SceneManager.LoadScene(respawnSceneName);

        if (health != null)
            health.Heal(health.GetMaxHealth());

        isDead = false;
    }

    private void ResetInventory()
    {
        if (InventoryManager.Instance == null) return;

        foreach (InventorySlot slot in InventoryManager.Instance.slots)
        {
            slot.Clear();
        }

        InventoryManager.Instance.ForceRefresh();
    }
}