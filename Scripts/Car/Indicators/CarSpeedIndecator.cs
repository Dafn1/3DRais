using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarSpeedIndecator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Text text;


    private void Update()
    {
        text.text = car.LinerVelocity.ToString("F0");
    }
}
