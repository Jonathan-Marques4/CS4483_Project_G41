using System.Collections;
using TMPro;
using UnityEngine;

public class DeathScreenUI : MonoBehaviour
{
    public static DeathScreenUI Instance;

    public GameObject panel;
    public TextMeshProUGUI deathText;
    public float displayTime = 5f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public IEnumerator ShowDeathScreen()
    {
        if (deathText != null)
            deathText.text = "OH NO!\nYou have died and lost all of your items.";

        if (panel != null)
            panel.SetActive(true);

        yield return new WaitForSeconds(displayTime);

        if (panel != null)
            panel.SetActive(false);
    }
}