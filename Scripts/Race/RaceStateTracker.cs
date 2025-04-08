using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour, IDependency<TrackpointCircuit>
{
    public event UnityAction PeparationStarted;
    public event UnityAction Started;
    public event UnityAction Complited;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapComplited;

    public Timer CountDownTimer => countdownTimer;

    private TrackpointCircuit trackpointCircuit;
    public void Construct(TrackpointCircuit trackpointCircuit) => this.trackpointCircuit = trackpointCircuit;
    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToComplited;

    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }


    private void Start()
    {
        
        StartState(RaceState.Preparation);
        countdownTimer.enabled = false;
        countdownTimer.Finished += OnCountTimerFinished;
        trackpointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackpointCircuit.LapComplited += OnLapComplited;
    }



    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountTimerFinished;
        trackpointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackpointCircuit.LapComplited -= OnLapComplited;
    }
    private void OnCountTimerFinished()
    {
        StartRase();
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }

    private void OnLapComplited(int lapAmmount)
    {
        if (trackpointCircuit.Type == TrackType.Sprint)
        {
            CompliteRase();
        }

        if (trackpointCircuit.Type == TrackType.Circular)
        {
            if (lapAmmount == lapsToComplited) CompliteRase();
            else CompleteLap(lapAmmount);

        }
    }

    public void LaunchPeparationStart()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CountDown);

        countdownTimer.enabled = true;
        PeparationStarted?.Invoke();
    }
    public void StartRase()
    {
        if (state != RaceState.CountDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }
    public void CompliteRase()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);
        Complited?.Invoke();
    }
    private void CompleteLap(int lapAmmount)
    {
        LapComplited?.Invoke(lapAmmount);
    }


}
