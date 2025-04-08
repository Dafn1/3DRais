using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarGearBoxIndecator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;

    private void Start()
    {
        car.GearChenged += OnGearChenged;
    }

    private void OnDestroy()
    {
        car.GearChenged -= OnGearChenged;
    }

    private void OnGearChenged(string gearName)
    {
        text.text = gearName;
    }
}
