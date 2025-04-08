using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    [SerializeField] Text text;

    private RaceTimeTracker TimeTracker;
    public void Construct(RaceTimeTracker obj) => TimeTracker = obj;


    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Complited += OnRaceComplited;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Complited -= OnRaceComplited;
    }

    private void OnRaceComplited()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnRaceStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void Update()
    {
        text.text = StringTime.SecondToTimeString(TimeTracker.CurrentTime);
    }
}
