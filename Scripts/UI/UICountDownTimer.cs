using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Text text;


    private Timer countDownTimer;

    private void Start()
    {
        raceStateTracker.PeparationStarted += OnPeparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
    }
    [SerializeField] private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnPeparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnPeparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }
    private void Update()
    {
        text.text = raceStateTracker.CountDownTimer.Value.ToString("F0");


        if (text.text == "0") text.text = "GO!";
    }


}
