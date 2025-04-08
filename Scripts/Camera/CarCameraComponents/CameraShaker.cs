using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : CarCameraComponent
{
  
    [SerializeField][Range(0f, 1f)] private float normalizeSpeedShake;

    [SerializeField] private float shakeAmount;
    private void Update()
    {
        if (car.NormalizeLinerVelocity >= normalizeSpeedShake)
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount * Time.deltaTime;
        }
    }
}
