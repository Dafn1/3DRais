using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceTimeTracker : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private float currentTime;

    public float CurrentTime => currentTime;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Complited += OnRaceComplited;

        enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Complited -= OnRaceComplited;
    }

    private void OnRaceStarted()
    {
        enabled = true;
        currentTime = 0;
    }

    private void OnRaceComplited()
    {
        enabled = false;
    }

    private void Update()
    {
        currentTime += Time.deltaTime;
    }
}
