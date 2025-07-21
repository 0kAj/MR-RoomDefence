using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEventHandler : MonoBehaviour
{
    public void StartGame()
    {
        // MenuScene = 0
        // GameScene = 1
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void BackToMainMenu()
    {
        // MenuScene = 0
        // GameScene = 1
        SceneManager.LoadScene(0);
    }
}
