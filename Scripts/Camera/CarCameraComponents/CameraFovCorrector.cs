using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFovCorrector : CarCameraComponent
{
  
    [SerializeField] private float minFildOfView;
    [SerializeField] private float maxFildOfView;

    private float defautFov;
    private void Start()
    {
        camera.fieldOfView = defautFov;
    }
    private void Update()
    {
        camera.fieldOfView = Mathf.Lerp(minFildOfView, maxFildOfView, car.NormalizeLinerVelocity);
    }
}
