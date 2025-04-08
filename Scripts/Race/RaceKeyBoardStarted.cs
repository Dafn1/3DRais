using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RaceKeyBoardStarted : MonoBehaviour, IDependency<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;

    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true) raceStateTracker.LaunchPeparationStart();
    }
}
