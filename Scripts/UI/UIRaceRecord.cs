using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecord : MonoBehaviour, IDependency<RaceResaultTime>, IDependency<RaceStateTracker>
{
    private RaceResaultTime raceResaultTime;
    public void Construct(RaceResaultTime obj) => raceResaultTime = obj;


    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;


    [SerializeField] private GameObject goldRecordObject;
    [SerializeField] private GameObject playerRecordObject;
    //[SerializeField] private GameObject panel;

    [SerializeField] private GameObject infoPanel;

    [SerializeField] private Text goldRecordTime;
    [SerializeField] private Text playerRecordTime;

   // [SerializeField] private Text playerRecordTimePanel;
    //[SerializeField] private Text GoldRecordTimePanel;


    private Text recordLable;


    private void Start()
    {
        raceStateTracker.Started += OnRaseStart;
        raceStateTracker.Complited += OnRaceComplited;

        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
       // panel.SetActive(false);
        infoPanel.SetActive(true);


    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaseStart;
        raceStateTracker.Complited -= OnRaceComplited;


    }
    

    private void OnRaseStart()
    {
        infoPanel.SetActive(false);
        if (raceResaultTime.PlayRecordTime > raceResaultTime.GoldTime || raceResaultTime.RecordWasSet == false)
        {
            goldRecordObject.SetActive(true);
            goldRecordTime.text = StringTime.SecondToTimeString(raceResaultTime.GoldTime);
          
        }
       

        if (raceResaultTime.RecordWasSet == true)
        {
            playerRecordObject.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeString(raceResaultTime.PlayRecordTime);
           
        }

       // GoldRecordTimePanel.text = StringTime.SecondToTimeString(raceResaultTime.GoldTime);
        //playerRecordTimePanel.text = StringTime.SecondToTimeString(raceResaultTime.PlayRecordTime);

    }

    private void OnRaceComplited()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
        //panel.SetActive(true);
        infoPanel.SetActive(false);


    }
}
