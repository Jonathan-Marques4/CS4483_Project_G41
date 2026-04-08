using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public bool IsGameplayBlocked()
    {
        if (MenuManager.Instance != null && MenuManager.Instance.IsMenuOpen())
            return true;

        if (EatPromptUI.Instance != null && EatPromptUI.Instance.IsPromptOpen())
            return true;

        if (SleepPromptUI.Instance != null && SleepPromptUI.Instance.IsPromptOpen())
            return true;
        
        if (ChestUI.Instance != null && ChestUI.Instance.IsChestOpen())
            return true;

        return false;
    }
}