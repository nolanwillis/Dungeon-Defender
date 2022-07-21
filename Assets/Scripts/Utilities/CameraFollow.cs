using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Target camera follows
    public Transform target;
    // Smooth speed (lower the value, slower the camera)
    [SerializeField] private float smoothSpeed = .125f;
    // Camera position offset
    [SerializeField] private Vector3 offset;
    // Camera velocity
    private Vector3 velocity;

    // Updates camera position
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, 
            desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
