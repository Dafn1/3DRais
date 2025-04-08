using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausePanel : MonoBehaviour, IDependency<Pause>
{
    [SerializeField] private GameObject panel;

    private Pause pause;
    public void Construct(Pause obj) => pause = obj;

    private void Start()
    {
        panel.SetActive(false);

        pause.PauseStateChange += OnPauseStateChange;
    }


    private void OnDestroy()
    {
        pause.PauseStateChange -= OnPauseStateChange;
    }

    private void OnPauseStateChange(bool isPause)
    {
        panel.SetActive(isPause);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true)
        {
            pause.ChangePauseState();
        }
    }

    public void Unpause()
    {
        pause.UnPauser();
    }

}
