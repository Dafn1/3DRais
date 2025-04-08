using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResaultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    public static string SaveMark = "_Player_best_time";

    public event UnityAction ResaultUpdated;

    [SerializeField] private float goldTime;
    private float playRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayRecordTime => playRecordTime;
    public float CurrentTime => currentTime;
    public bool RecordWasSet => playRecordTime != 0;


    private RaceTimeTracker TimeTracker;
    public void Construct(RaceTimeTracker obj) => TimeTracker = obj;


    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Awake()
    {
        load();
    }

    private void Start()
    {

        raceStateTracker.Complited += OnRaceComplited;
    }
    private void OnDestroy()
    {
        raceStateTracker.Complited -= OnRaceComplited;
    }
    private void OnRaceComplited()
    {
        float absoluteRecord = GetAbsoluteRecord();

        if (TimeTracker.CurrentTime < absoluteRecord || playRecordTime == 0)
        {
            playRecordTime = TimeTracker.CurrentTime;

            Save();
        }
        currentTime = TimeTracker.CurrentTime;

        ResaultUpdated?.Invoke();
    }

    public float GetAbsoluteRecord()
    {
        if (playRecordTime < goldTime && playRecordTime != 0)
        {
            return playRecordTime;
        }
        else
            return goldTime;
    }

    private void load()
    {
        playRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
      
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playRecordTime);
    }
}
