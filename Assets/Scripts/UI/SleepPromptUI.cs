using TMPro;
using UnityEngine;

public class SleepPromptUI : MonoBehaviour
{
    public static SleepPromptUI Instance;

    public GameObject panel;
    public TextMeshProUGUI promptText;

    private BedInteraction pendingBed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        HidePrompt();
    }

    public void ShowPrompt(BedInteraction bed)
    {
        if (bed == null) return;

        pendingBed = bed;

        if (panel != null)
            panel.SetActive(true);

        if (promptText != null)
            promptText.text = "Do you want to sleep for 8 hours? \n (Recovers Energy and 25 HP)";

        if (TimeManager.Instance != null)
            TimeManager.Instance.SetTimePaused(true);
    }

    public void ConfirmSleep()
    {
        if (pendingBed != null)
        {
            pendingBed.Sleep();
        }

        HidePrompt();
    }

    public void CancelSleep()
    {
        HidePrompt();
    }

    public void HidePrompt()
    {
        pendingBed = null;

        if (panel != null)
            panel.SetActive(false);

        if (TimeManager.Instance != null)
        {
            if (MenuManager.Instance == null || !MenuManager.Instance.IsMenuOpen())
            {
                TimeManager.Instance.SetTimePaused(false);
            }
        }
    }

    public bool IsPromptOpen()
    {
        return panel != null && panel.activeSelf;
    }
}