﻿using UnityEngine;
using System.Collections;
using System;

public class WalkingState : BaseState {

    public override void Construct ()
    {
        base.Construct ();

        motor.VerticalVelocity = 0;
    }

    public override Vector3 ProcessMotion (Vector3 input)
    {
        ApplySpeed (ref input, motor.Speed);

        return input;
    }

    public override Quaternion ProcessRotation (Vector3 input)
    {
        return Quaternion.FromToRotation (Vector3.forward, input);
    }

    public override void Transition ()
    {
        // If we're not grounded
        if (!motor.Grounded ())
            motor.ChangeState ("FallingState");

        // If space is pressed, jump
        if (Input.GetButtonDown ("Jump"))
            motor.ChangeState ("JumpingState");
    }
}
