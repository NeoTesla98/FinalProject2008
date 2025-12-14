using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{ 
    public void StartGame()
    {
        SceneManager.LoadScene("Main Menu");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
   

   
