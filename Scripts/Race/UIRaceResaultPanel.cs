using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceResaultPanel : MonoBehaviour, IDependency<RaceResaultTime>
{
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TextMeshProUGUI recordTime;
    [SerializeField] private TextMeshProUGUI currentTime;

    private RaceResaultTime raceResaultTime;
    public void Construct(RaceResaultTime obj) => raceResaultTime = obj;

    private void Start()
    {
        resultPanel.SetActive(false);
        raceResaultTime.ResaultUpdated += OnUpdateResault;
    }
    private void OnDestroy()
    {
        raceResaultTime.ResaultUpdated -= OnUpdateResault;
    }

    private void OnUpdateResault()
    {
        resultPanel.SetActive(true);

        recordTime.text = StringTime.SecondToTimeString(raceResaultTime.GetAbsoluteRecord());
        currentTime.text = StringTime.SecondToTimeString(raceResaultTime.CurrentTime);
    }
}
