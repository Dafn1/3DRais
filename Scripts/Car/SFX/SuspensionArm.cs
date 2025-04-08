using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuspensionArm : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float factor;

    private float beastOffset;
    private void Start()
    {
        beastOffset = target.localPosition.y;
    }

    private void Update()
    {
        transform.localEulerAngles = new Vector3(0, 0, (target.localPosition.y - beastOffset) * factor);
    }
}
