using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneDependencies : Dependency
{
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;
    [SerializeField] private RaceTimeTracker timeTracker;
    [SerializeField] private RaceResaultTime timeResault;


    protected override void BindAll(MonoBehaviour monoBehaviourInScene)
    {
        Bind<RaceStateTracker>(raceStateTracker, monoBehaviourInScene);
        Bind<CarInputControl>(carInputControl, monoBehaviourInScene);
        Bind<TrackpointCircuit>(trackpointCircuit, monoBehaviourInScene);
        Bind<Car>(car, monoBehaviourInScene);
        Bind<CarCameraController>(carCameraController, monoBehaviourInScene);
        Bind<RaceTimeTracker>(timeTracker, monoBehaviourInScene);
        Bind<RaceResaultTime>(timeResault, monoBehaviourInScene);

    }

    private void Awake()
    {
        FinedAllObjectToBind();
    }
}
