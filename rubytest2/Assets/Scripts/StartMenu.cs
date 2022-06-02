using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StartMenu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit ();
        Debug.Log ("Quit button pushed");
    
    }

    public void StartGame()
    {
        SceneManager.LoadScene ("mainscene");
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene ("HowToPlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene ("MainMenu");
    }
}
