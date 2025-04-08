using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public enum TrackType
{
    Circular,
    Sprint
}

public class TrackpointCircuit : MonoBehaviour
{
    public event UnityAction<TrackPoint> TrackPointTriggered;
    public event UnityAction<int> LapComplited;

    [SerializeField] private TrackType type;
    public TrackType Type => type;

    private TrackPoint[] points;
    private int LapsCompslited = -1;


    private void Awake()
    {
        BildCircuit();
    }
    private void Start()
    {


        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered += OnTrackPointTriggered;
        }
        points[0].AssingAsTarget();
    }


    private void OnDestroy()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered -= OnTrackPointTriggered;
        }
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.Next?.AssingAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if (trackPoint.IsLast == true)
        {
            LapsCompslited++;
            if (type == TrackType.Sprint)
            {
                LapComplited?.Invoke(LapsCompslited);
            }

            if (type == TrackType.Circular)
            {
                if (LapsCompslited > 0)
                {
                    LapComplited?.Invoke(LapsCompslited);
                }
            }
        }
    }

    [ContextMenu(nameof(BildCircuit))]
    private void BildCircuit()
    {
        points = TrackCircultBilder.Build(transform, type);
    }
}
