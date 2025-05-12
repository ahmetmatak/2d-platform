using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratsScreen : MonoBehaviour
{
    public float delay=3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Invoke("LevelSelect",delay);
    }

    void LevelSelect(){
        SceneManager.LoadScene("LevelSelect");
    }
}
