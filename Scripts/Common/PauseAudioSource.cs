using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioSource : MonoBehaviour, IDependency<Pause>
{


    private new AudioSource audio;

    private Pause pause;
    public void Construct(Pause obj) => pause = obj;

    private void Start()
    {
        audio = GetComponent<AudioSource>();

        pause.PauseStateChange += OnPausePauseStateChange;
    }

    private void OnDestroy()
    {
        pause.PauseStateChange -= OnPausePauseStateChange;
    }

    private void OnPausePauseStateChange(bool pause)
    {


        if (pause == false)
        {
            audio.Play();
        }
        if (pause == true)
        {
            audio.Stop();
        }

    }
}
