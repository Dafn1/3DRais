using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private Car car;
    [SerializeField] private new Camera camera;
    [SerializeField] private CameraFollow follower;
    [SerializeField] private CameraShaker shaker;
    [SerializeField] private CameraFovCorrector fovCorrector;
    [SerializeField] private CameraPathFollower pathFollower;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;



    private void Awake()
    {
        follower.SetPropperties(car, camera);
        shaker.SetPropperties(car, camera);
        fovCorrector.SetPropperties(car, camera);
    }

    private void Start()
    {
        raceStateTracker.PeparationStarted += OnPeparationStarted;
        raceStateTracker.Complited += OnComplited;

        follower.enabled = false;
        pathFollower.enabled = true;
    }
    private void OnDestroy()
    {
        raceStateTracker.PeparationStarted -= OnPeparationStarted;
        raceStateTracker.Complited -= OnComplited;

    }

    private void OnPeparationStarted()
    {
        follower.enabled = true;
        pathFollower.enabled = false;
    }


    private void OnComplited()
    {
        pathFollower.enabled = true;
        pathFollower.StartMoveToNearestPoint();
        pathFollower.SetLookTarget(car.transform);

        follower.enabled = false;
    }

}
