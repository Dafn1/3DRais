using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string MainMenuSceneTirle = "MainMenu";

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneTirle);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
}
