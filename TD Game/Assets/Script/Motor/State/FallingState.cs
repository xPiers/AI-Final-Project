using UnityEngine;
using System.Collections;
using System;

public class FallingState : BaseState {

    public override Vector3 ProcessMotion (Vector3 input)
    {
        ApplySpeed (ref input, motor.Speed);

        ApplyGravity (ref input, motor.Gravity);

        return input;
    }

    public override void Transition ()
    {
        // If we're not grounded
        if (motor.Grounded ())
            motor.ChangeState ("WalkingState");
    }
}
