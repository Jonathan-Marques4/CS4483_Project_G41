using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public GameObject tabMenuRoot;
    public GameObject inventoryPanel;
    public GameObject savePanel;
    public GameObject settingsPanel;
    public GameObject exitPanel;
    public GameObject menuButtons;
    public GameObject closeButton;
    public GameObject hotbarPanel;

    private bool menuOpen = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start(){

        CloseAllMenus();
    }

    private void Update(){

        if (Input.GetKeyDown(KeyCode.Tab)){
            
            ToggleInventoryMenu();
        }

        if (menuOpen && Input.GetKeyDown(KeyCode.Escape)){

            CloseAllMenus();
        }
    }

    public void ToggleInventoryMenu(){

        if (menuOpen && inventoryPanel.activeSelf){

            CloseAllMenus();
        }
        else{

            OpenMenu(inventoryPanel);
        }
    }

    public void OpenMenu(GameObject targetPanel)
    {
        tabMenuRoot.SetActive(true);

        inventoryPanel.SetActive(false);
        savePanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);

        targetPanel.SetActive(true);

        if (hotbarPanel != null)
            hotbarPanel.SetActive(false);

        menuOpen = true;

        if (TimeManager.Instance != null)
            TimeManager.Instance.SetTimePaused(true);

    }

    public void CloseAllMenus(){

        tabMenuRoot.SetActive(false);

        inventoryPanel.SetActive(false);
        savePanel.SetActive(false);
        settingsPanel.SetActive(false);
        exitPanel.SetActive(false);

        if (hotbarPanel != null)
            hotbarPanel.SetActive(true);

        menuOpen = false;

        if (TimeManager.Instance != null)
            TimeManager.Instance.SetTimePaused(false);
    }

    public bool IsMenuOpen(){

        return menuOpen;
    }
}