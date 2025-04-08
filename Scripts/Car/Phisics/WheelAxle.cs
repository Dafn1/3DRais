using System;
using UnityEngine;


[Serializable]
public class WheelAxle
{
    [SerializeField] private WheelCollider leftWhellCollider;
    [SerializeField] private WheelCollider rightWhellCollider;

    [SerializeField] private Transform leftWheelMesh;
    [SerializeField] private Transform rightWheelMesh;

    [SerializeField] private bool isMotor;
    [SerializeField] private bool isSteer;



    [SerializeField] private float wheelWinds;

    [SerializeField] private float AntiRollForce;

    [SerializeField] private float additionalWheelDownForce;

    [SerializeField] private float baseForwardStiffnes = 1.5f;
    [SerializeField] private float stabiliyForwardFactor = 1.0f;

    [SerializeField] private float baseSidewaysStiffnes = 2.0f;
    [SerializeField] private float stabiliySidewaysFactor = 0.1f;

    private WheelHit leftWheelHit;
    private WheelHit rightWheelHit;

    public bool IsMotor => isMotor;
    public bool IsSteer => isSteer;

    //public API
    public void Update()
    {
        UpdateWheelHits();
        //приминяем стабилизатор поперечной устойчивости
        ApplyAntiRoll();
        //пррижимная сила колес
        ApplyDownForce();
        //коректиролвка силы трения колес
        CorrectStiffness();

        SyncMeshTransform();
    }



    public float GetAvarageRpm()
    {
        return (leftWhellCollider.rpm + rightWhellCollider.rpm) * 0.5f;
    }

    public float GetRadius()
    {
        return leftWhellCollider.radius;
    }

    public void ConfigureVehicleSubsteps(float speedThreshold, int speedBelowThreshold, int stepAboveThreshold)
    {
        leftWhellCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepAboveThreshold);
        rightWhellCollider.ConfigureVehicleSubsteps(speedThreshold, speedBelowThreshold, stepAboveThreshold);
    }

    public void ApplySteerAngle(float steerAngle, float whellBaseLength)
    {
        if (isSteer == false) return;
        //угол Аккермана
        float radius = Mathf.Abs(whellBaseLength * Mathf.Tan(Mathf.Deg2Rad * (90 - Math.Abs(steerAngle))));
        float anglleSing = Mathf.Sign(steerAngle);
        if (steerAngle > 0)
        {
            leftWhellCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLength / (radius + (wheelWinds * 0.5f))) * anglleSing;
            rightWhellCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLength / (radius - (wheelWinds * 0.5f))) * anglleSing; ;
        }
        else if (steerAngle < 0)
        {
            leftWhellCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLength / (radius - (wheelWinds * 0.5f))) * anglleSing; ;
            rightWhellCollider.steerAngle = Mathf.Rad2Deg * Mathf.Atan(whellBaseLength / (radius + (wheelWinds * 0.5f))) * anglleSing; ;
        }
        else
        {
            leftWhellCollider.steerAngle = 0;
            rightWhellCollider.steerAngle = 0;
        }
        //end

    }

    public void ApplyMotorTorque(float motorTorque)
    {
        if (isMotor == false) return;

        leftWhellCollider.motorTorque = motorTorque;
        rightWhellCollider.motorTorque = motorTorque;
    }
    public void ApplyBrakTorque(float brakeTorque)
    {

        leftWhellCollider.brakeTorque = brakeTorque;
        rightWhellCollider.brakeTorque = brakeTorque;
    }


    //private
    private void UpdateWheelHits()
    {
        leftWhellCollider.GetGroundHit(out leftWheelHit);
        rightWhellCollider.GetGroundHit(out rightWheelHit);
    }

    private void CorrectStiffness()
    {
        WheelFrictionCurve leftForward = leftWhellCollider.forwardFriction;
        WheelFrictionCurve rightForward = rightWhellCollider.forwardFriction;

        WheelFrictionCurve leftSideways = leftWhellCollider.sidewaysFriction;
        WheelFrictionCurve rightSideways = rightWhellCollider.sidewaysFriction;

        leftForward.stiffness = baseForwardStiffnes + Mathf.Abs(leftWheelHit.forwardSlip) * stabiliyForwardFactor;
        rightForward.stiffness = baseForwardStiffnes + Mathf.Abs(rightWheelHit.forwardSlip) * stabiliyForwardFactor;

        leftSideways.stiffness = baseSidewaysStiffnes + Mathf.Abs(leftWheelHit.sidewaysSlip) * stabiliySidewaysFactor;
        rightSideways.stiffness = baseSidewaysStiffnes + Mathf.Abs(rightWheelHit.sidewaysSlip) * stabiliySidewaysFactor;

        leftWhellCollider.forwardFriction = leftForward;
        rightWhellCollider.forwardFriction = rightForward;

        leftWhellCollider.sidewaysFriction = leftSideways;
        rightWhellCollider.sidewaysFriction = rightSideways;

    }

    private void ApplyDownForce()
    {
        if (leftWhellCollider.isGrounded == true)
        {
            leftWhellCollider.attachedRigidbody.AddForceAtPosition(leftWheelHit.normal * -additionalWheelDownForce * leftWhellCollider.attachedRigidbody.velocity.magnitude, leftWhellCollider.transform.position);

            rightWhellCollider.attachedRigidbody.AddForceAtPosition(rightWheelHit.normal * -additionalWheelDownForce * rightWhellCollider.attachedRigidbody.velocity.magnitude, rightWhellCollider.transform.position);
        }
    }

    private void ApplyAntiRoll()
    {
        float travelL = 1.0f;
        float travelR = 1.0f;

        if (leftWhellCollider.isGrounded == true)
        {
            travelL = (-leftWhellCollider.transform.InverseTransformPoint(leftWheelHit.point).y - leftWhellCollider.radius) / leftWhellCollider.suspensionDistance;
        }

        if (rightWhellCollider.isGrounded == true)
        {
            travelR = (-rightWhellCollider.transform.InverseTransformPoint(rightWheelHit.point).y - rightWhellCollider.radius) / rightWhellCollider.suspensionDistance;
        }
        float forceDir = (travelL - travelR);

        if (leftWhellCollider.isGrounded == true)
        {
            leftWhellCollider.attachedRigidbody.AddForceAtPosition(leftWhellCollider.transform.up * -forceDir * AntiRollForce, leftWhellCollider.transform.position);
        }



        if (rightWhellCollider.isGrounded == true)
        {
            rightWhellCollider.attachedRigidbody.AddForceAtPosition(rightWhellCollider.transform.up * forceDir * AntiRollForce, rightWhellCollider.transform.position);
        }

    }



    //private
    private void SyncMeshTransform()
    {
        updateWheelTransform(leftWhellCollider, leftWheelMesh);
        updateWheelTransform(rightWhellCollider, rightWheelMesh);
    }

    private void updateWheelTransform(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);

        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }
}
