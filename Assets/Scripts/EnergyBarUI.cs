using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyBarUI : MonoBehaviour
{

    public Slider energySlider;
    public TextMeshProUGUI energyText;


    private void Start(){

        if (PlayerEnergy.Instance != null){

            PlayerEnergy.Instance.OnEnergyChanged += UpdateEnergyUI;
            UpdateEnergyUI();
        }
    }

    private void OnDestroy(){

        if (PlayerEnergy.Instance != null){

            PlayerEnergy.Instance.OnEnergyChanged -= UpdateEnergyUI;
        }
    }


    private void UpdateEnergyUI(){

        if (PlayerEnergy.Instance == null) return;

        if (energySlider != null){

            energySlider.maxValue = PlayerEnergy.Instance.maxEnergy;
            energySlider.value = PlayerEnergy.Instance.currentEnergy;
        }

        if (energyText != null){

            energyText.text = "Energy: " + $"{PlayerEnergy.Instance.currentEnergy}/{PlayerEnergy.Instance.maxEnergy}";
        }
    }
}