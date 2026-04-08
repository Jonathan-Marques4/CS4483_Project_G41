using UnityEngine;

public class BedSleep : MonoBehaviour
{
    public int sleepHours = 8;

    public void Sleep(){

        if (PlayerEnergy.Instance != null){

            PlayerEnergy.Instance.RestoreFullEnergy();
        }

        if (TimeManager.Instance != null){
            
            TimeManager.Instance.AdvanceHours(sleepHours);
        }

        Debug.Log("Player slept and restored full energy.");
    }
}