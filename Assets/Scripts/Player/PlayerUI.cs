using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PlayerStats playerStats;
    void Awake()
    {
        playerStats = PlayerStats.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "HP: " + playerStats.currentHp + "/" + playerStats.maxHp;
    }
}
