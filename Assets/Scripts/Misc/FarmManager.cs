using UnityEngine;

public class FarmManager : MonoBehaviour{

    public FarmTile[] farmTiles;

    private void Start(){

        if (TimeManager.Instance != null){

            TimeManager.Instance.OnDayChanged += HandleDayChanged;
        }
    }

    private void OnDestroy(){

        if (TimeManager.Instance != null){

            TimeManager.Instance.OnDayChanged -= HandleDayChanged;
        }
    }

    private void HandleDayChanged(){

        for (int i = 0; i < farmTiles.Length; i++){

            if (farmTiles[i] != null){
                
                farmTiles[i].GrowCropOneDay();
            }
        }
    }
}