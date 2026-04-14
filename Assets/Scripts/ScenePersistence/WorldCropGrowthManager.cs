using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldCropGrowthManager : MonoBehaviour
{
    public string farmSceneName = "SpawnArea";

    private void Start()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnDayChanged += HandleDayChanged;
        }
    }

    private void OnDestroy()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnDayChanged -= HandleDayChanged;
        }
    }

    private void HandleDayChanged()
    {
        string activeScene = SceneManager.GetActiveScene().name;

        if (activeScene != farmSceneName)
        {
            if (SceneStateManager.Instance != null)
            {
                SceneStateManager.Instance.AdvanceAllSavedCropsOneDay();
                Debug.Log("Advanced saved farm crops by one day.");
            }
        }
    }
}