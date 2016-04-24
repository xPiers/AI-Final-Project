using UnityEngine;
using System.Collections;
using System;

public class FirstPersonCamera : BaseCameraState {

    private const float Y_ANGLE_MIN = -75.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    private float offset = 1.0f;

    private float currentX = 0.0f,
        currentY = 0.0f,
        sensitivityX = 4.0f,
        sensitivityY = 1.0f;

    public override Vector3 ProcessMotion (Vector3 input)
    {
        return transform.position + (transform.up * offset);
    }

    public override Quaternion ProcessRotation (Vector3 input)
    {
        currentX += input.x * sensitivityX;
        currentY += input.z * sensitivityY;

        // Clamp currentY
        currentY = ClampAngle (currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        return Quaternion.Euler (currentY, currentX, 0);
    }
}
