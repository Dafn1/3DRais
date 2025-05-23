using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CarCameraController))]
public abstract class CarCameraComponent : MonoBehaviour
{
    protected Car car;
    protected new Camera camera;


    public virtual void SetPropperties(Car car, Camera camera)
    {
        this.car = car;
        this.camera = camera;
    }
}
