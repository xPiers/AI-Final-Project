using UnityEngine;
using System.Collections;
using System;

public class ThirdPersonCamera : BaseCameraState
{

    private const float Y_ANGLE_MIN = -75.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    private Transform lookAt;
    private Transform cameraContainer;

    private Vector3 offset = Vector3.up;
    private float distance = 10.0f;

    private float currentX = 0.0f,
        currentY = 0.0f,
        sensitivityX = 4.0f,
        sensitivityY = 1.0f;

    public override void Construct ()
    {
        base.Construct ();

        lookAt = transform;
        cameraContainer = motor.CameraContainer;
    }

    public override Vector3 ProcessMotion (Vector3 input)
    {
        currentX += input.x * sensitivityX;
        currentY += input.z * sensitivityY;

        // Clamp currentY
        currentY = ClampAngle (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        return CalculatePosition();
    }

    public override Quaternion ProcessRotation (Vector3 input)
    {
        cameraContainer.LookAt (lookAt.position + offset);
        return cameraContainer.rotation;
    }

    private Vector3 CalculatePosition ()
    {
        Vector3 direction = new Vector3 (0, 0, -distance);
        Quaternion rotation = Quaternion.Euler (currentY, currentX, 0);

        return (lookAt.position + offset) + rotation * direction;
    }
}
