using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalIdependencies : Dependency
{
    [SerializeField] private Pause pause;
    private static GlobalIdependencies instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);


        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<Pause>(pause, monoBehaviourInScene);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FinedAllObjectToBind();

    }
}
