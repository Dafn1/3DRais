using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve breakCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBreakStrength = 0.5f;


    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbreak;


    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottle();

        UpdateSteer();

        UpdateAutoBrake();

        //debug

        if (Input.GetKeyDown(KeyCode.E))
            car.GearUp();


        if (Input.GetKeyDown(KeyCode.Q))
            car.DownGear();

    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }



    private void UpdateThrottle()
    {


        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;

        }
        else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToReverseGear();
        }
        if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f)
        {
            car.ShiftToFirstGear();
        }

    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");

        horizontalAxis = Input.GetAxis("Horizontal");

        handbreak = Input.GetAxis("Jump");

    }
    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handbreak = 0;

        car.ThrottleControl = 0;
        car.BrakeControl = 1;
        car.SteerControl = 0;
    }

    public void Stop()
    {
        Reset();

        car.BrakeControl = 1;
    }

    private void UpdateAutoBrake()
    {
        if (verticalAxis == 0)
        {
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBreakStrength;
        }
    }

  
}
