using UnityEngine;
using System;

public class PlayerLevelSystem : MonoBehaviour
{
    public static PlayerLevelSystem Instance;

    [Header("Level Settings")]
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNextLevel = 100;
    public int xpGrowthPerLevel = 50;

    [Header("Level Up Rewards")]
    public float oxygenIncreasePerLevel = 20f;
    public int energyIncreasePerLevel = 10;
    public bool refillEnergyOnLevelUp = true;
    public bool refillOxygenOnLevelUp = true;

    public event Action OnXPChanged;
    public event Action OnLevelChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddXP(int amount)
    {
        if (amount <= 0) return;

        currentXP += amount;
        OnXPChanged?.Invoke();

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }

        OnXPChanged?.Invoke();
    }

    private void LevelUp()
    {
        currentLevel++;

        // Increase max oxygen
        if (OxygenManager.Instance != null)
        {
            OxygenManager.Instance.maxOxygen += oxygenIncreasePerLevel;

            if (refillOxygenOnLevelUp)
                OxygenManager.Instance.currentOxygen = OxygenManager.Instance.maxOxygen;
            else
                OxygenManager.Instance.currentOxygen = Mathf.Min(
                    OxygenManager.Instance.currentOxygen,
                    OxygenManager.Instance.maxOxygen
                );
        }

        // Increase max energy
        if (PlayerEnergy.Instance != null)
        {
            PlayerEnergy.Instance.maxEnergy += energyIncreasePerLevel;

            if (refillEnergyOnLevelUp)
                PlayerEnergy.Instance.currentEnergy = PlayerEnergy.Instance.maxEnergy;
            else
                PlayerEnergy.Instance.currentEnergy = Mathf.Min(
                    PlayerEnergy.Instance.currentEnergy,
                    PlayerEnergy.Instance.maxEnergy
                );

            PlayerEnergy.Instance.ForceNotify();
        }

        xpToNextLevel += xpGrowthPerLevel;

        Debug.Log("Level Up! Reached level " + currentLevel);

        OnLevelChanged?.Invoke();
        OnXPChanged?.Invoke();
    }

    public float GetXPProgress()
    {
        if (xpToNextLevel <= 0) return 0f;
        return (float)currentXP / xpToNextLevel;
    }
}