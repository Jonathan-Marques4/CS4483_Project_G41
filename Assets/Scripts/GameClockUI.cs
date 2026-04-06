using UnityEngine;
using TMPro;

public class GameClockUI : MonoBehaviour{

    public TextMeshProUGUI clockText;

    private void Start(){

        if (TimeManager.Instance != null){

            TimeManager.Instance.OnTimeChanged += UpdateClockDisplay;
            UpdateClockDisplay();
        }
    }

    private void OnDestroy(){

        if (TimeManager.Instance != null){

            TimeManager.Instance.OnTimeChanged -= UpdateClockDisplay;
        }
    }

    private void UpdateClockDisplay(){
        
        if (TimeManager.Instance == null || clockText == null) return;

        clockText.text =
            $"{TimeManager.Instance.GetFormattedDay()}\n{TimeManager.Instance.GetFormattedTime()}";
    }
}