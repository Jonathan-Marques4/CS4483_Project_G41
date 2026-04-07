using UnityEngine;

public class GameMenuActions : MonoBehaviour{

    public void ExitGame(){

        Application.Quit();
    }

    public void CloseMenu(){

        if (MenuManager.Instance != null){
            
            MenuManager.Instance.CloseAllMenus();
        }
    }
}