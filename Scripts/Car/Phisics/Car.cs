using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassing))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChenged;

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTourqueCurve;
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineTorque;
    [SerializeField] private float engineRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;


    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRario;

    [SerializeField] private int selectedGearIndex;

    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;

    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;


    [SerializeField] private int maxSpeed;

    private CarChassing chassis;
    public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassing>().Rigidbody : chassis.Rigidbody;





    public float LinerVelocity => chassis.LinerVelocity;
    public float NormalizeLinerVelocity => chassis.LinerVelocity / maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;


    [SerializeField] private float linerVelocity;
    public float ThrottleControl;
    public float SteerControl;
    public float BrakeControl;


    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;


    private void Start()
    {
        chassis = GetComponent<CarChassing>();
    }

    private void Update()
    {
        linerVelocity = LinerVelocity;

        UpdateEngineTorque();

        AutoGearShift();

        if (LinerVelocity >= maxSpeed)
        {
            engineTorque = 0;
        }

        chassis.motorTorque = engineTorque * ThrottleControl;
        chassis.steerAngle = maxSteerAngle * SteerControl;
        chassis.brakeTorque = maxBrakeTorque * BrakeControl;


    }

    //gearBox

    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "R";

        if (selectedGear == 0) return "N";

        return (selectedGearIndex + 1).ToString();
    }

    public void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm)
            GearUp();

        if (engineRpm < downShiftEngineRpm)
            DownGear();
    }

    public void GearUp()
    {
        ShiftGear(selectedGearIndex + 1);
    }

    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }

    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;
        GearChenged?.Invoke(GetSelectedGearName());
    }

    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }

    public void ShiftToNetral()
    {
        selectedGear = 0;
        GearChenged?.Invoke(GetSelectedGearName());
    }



    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;
        GearChenged?.Invoke(GetSelectedGearName());
    }


    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAverageRpm() * selectedGear * finalDriveRario);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTourqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque * finalDriveRario * Mathf.Sign(selectedGear) * gears[0];
    }
    public void Reset()
    {
        chassis.Reset();

        chassis.motorTorque = 0;
        chassis.brakeTorque = 0;
        chassis.steerAngle = 0;

        ThrottleControl = 0;
        BrakeControl = 0;
        SteerControl = 0;
    }
    public void Respawn(Vector3 position, Quaternion rotation)
    {
        Reset();

        transform.position = position;
        transform.rotation = rotation;
    }
}
