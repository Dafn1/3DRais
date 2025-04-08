using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    public event UnityAction<bool> PauseStateChange;

    private bool isPause;
    public bool IsPause => isPause;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        UnPauser();
    }

    public void ChangePauseState()
    {
        if (isPause == true)
            UnPauser();
        else
            Pauser();
    }

    public void Pauser()
    {
        if (isPause == true) return;

        Time.timeScale = 0;

        isPause = true;
        PauseStateChange?.Invoke(isPause);
    }
    public void UnPauser()
    {
        if (isPause == false) return;

        Time.timeScale = 1;

        isPause = false;
        PauseStateChange?.Invoke(isPause);
    }

}
