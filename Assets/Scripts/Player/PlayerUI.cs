using UnityEngine;
using TMPro;
public class PlayerUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            text.text = "HP: " + PlayerStats.Instance.currentHp + "/" + PlayerStats.Instance.maxHp;
        }
    }
}
