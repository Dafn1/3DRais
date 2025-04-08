using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EngineSound : MonoBehaviour
{
    [SerializeField] private Car car;

    [SerializeField] private float pitchModifire;
    [SerializeField] private float voliumModifire;
    [SerializeField] private float rpmModifire;


    [SerializeField] private float basePitch = 1.0f;
    [SerializeField] private float baseVolume = 0.4f;


    private AudioSource engineAudioSource;

    private void Start()
    {
        engineAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        engineAudioSource.pitch = basePitch + pitchModifire * ((car.EngineRpm / car.EngineMaxRpm) * rpmModifire);
        engineAudioSource.pitch = baseVolume + voliumModifire * (car.EngineRpm / car.EngineMaxRpm);
    }

}
