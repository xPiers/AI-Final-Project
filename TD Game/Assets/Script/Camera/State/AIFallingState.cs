using UnityEngine;
using System.Collections;

public class AIFallingState : FallingState {

    public override void Transition ()
    {
        // If we're not grounded
        if (motor.Grounded ())
            motor.ChangeState ("AIWalkingState");
    }
}
