using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaseInputController : MonoBehaviour,IDependency<CarInputControl>,IDependency<RaceStateTracker>
{


    private CarInputControl carCopntrol;
    public void Construct(CarInputControl obj) => carCopntrol = obj;



    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Complited += OnRaseFinished;

        carCopntrol.enabled = false;
    }


    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Complited -= OnRaseFinished;
    }

    private void OnRaceStarted()
    {
        carCopntrol.enabled = true;
    }

    private void OnRaseFinished()
    {
        carCopntrol.Stop();
        carCopntrol.enabled = false;
    }
}
