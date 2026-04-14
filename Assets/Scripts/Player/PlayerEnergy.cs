using UnityEngine;
using System;

public class PlayerEnergy : MonoBehaviour
{
    public static PlayerEnergy Instance;

    [Header("Energy Settings")]
    public int maxEnergy = 100;
    public int currentEnergy = 100;

    public event Action OnEnergyChanged;
    public event Action OnEnergyDepleted;

    private void Awake(){
        
        if (Instance != null && Instance != this){

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start(){

        currentEnergy = maxEnergy;
        NotifyEnergyChanged();
    }

    public bool UseEnergy(int amount){

        if (amount <= 0) return true;

        if (currentEnergy < amount){

            return false;
        }

        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);

        NotifyEnergyChanged();

        if (currentEnergy <= 0){

            OnEnergyDepleted?.Invoke();
        }

        return true;
    }

    public void RestoreEnergy(int amount){

        if (amount <= 0) return;

        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        NotifyEnergyChanged();
    }

    public void RestoreFullEnergy(){

        currentEnergy = maxEnergy;
        NotifyEnergyChanged();
    }

    public float GetEnergyPercent(){

        if (maxEnergy <= 0) return 0f;
        return (float)currentEnergy / maxEnergy;
    }

    private void NotifyEnergyChanged(){

        OnEnergyChanged?.Invoke();
    }

    public void ForceNotify()
    {
        NotifyEnergyChanged();
    }
    
}