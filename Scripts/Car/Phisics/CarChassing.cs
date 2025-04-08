using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarChassing : MonoBehaviour
{
    [SerializeField] private WheelAxle[] wheelAxels;
    [SerializeField] private float whellBaseLength;
    [SerializeField] private Transform centerOfMass;

    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor;

    [Header("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>() : rigidbody;

    public float motorTorque;
    public float brakeTorque;
    public float steerAngle;

    public float LinerVelocity => rigidbody.velocity.magnitude * 3.6f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        if (centerOfMass != null)
        {
            rigidbody.centerOfMass = centerOfMass.localPosition;
        }
        for (int i = 0; i < wheelAxels.Length; i++)
        {
            wheelAxels[i].ConfigureVehicleSubsteps(50, 50, 50);
        }
    }

    private void FixedUpdate()
    {
        UpdateAngularDrag();

        UpdateDownForce();

        UpdateWheelAxles();
    }

    public float GetAverageRpm()
    {
        float sum = 0;

        for (int i = 0; i < wheelAxels.Length; i++)
        {
            sum += wheelAxels[i].GetAvarageRpm();
        }
        return sum / wheelAxels.Length;
    }

    public float GetWheelSpeed()
    {
        return GetAverageRpm() * wheelAxels[0].GetRadius() * 2 * 0.1885f;
    }

    private void UpdateAngularDrag()
    {
        rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinerVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateDownForce()
    {
        float downForce = Mathf.Clamp(downForceFactor * LinerVelocity, downForceMin, downForceMax);
        rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateWheelAxles()
    {
        int ammountMotorWheel = 0;

        for (int i = 0; i < wheelAxels.Length; i++)
        {
            if (wheelAxels[i].IsMotor == true)
            {
                ammountMotorWheel += 2;
            }
        }


        for (int i = 0; i < wheelAxels.Length; i++)
        {
            wheelAxels[i].Update();

            wheelAxels[i].ApplyMotorTorque(motorTorque / ammountMotorWheel);
            wheelAxels[i].ApplySteerAngle(steerAngle, whellBaseLength);
            wheelAxels[i].ApplyBrakTorque(brakeTorque);
        }

    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
