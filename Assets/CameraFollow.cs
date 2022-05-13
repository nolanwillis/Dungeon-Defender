using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target camera follows
    [SerializeField] private Transform target;
    // Smooth speed (lower the value, slower the camera)
    [SerializeField] private float smoothSpeed = .125f;
    // Camera position offset
    [SerializeField] private Vector3 offset;

    // FixedUpdate, called at same rate as physics engine
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }

}
