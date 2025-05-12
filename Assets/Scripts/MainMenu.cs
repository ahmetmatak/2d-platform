using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("LevelSelect");
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void ReturnToMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
