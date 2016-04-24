using UnityEngine;
using System.Collections;

public class AIWalkingState : WalkingState {

    public override void Transition ()
    {
        // If we're not grounded
        if (!motor.Grounded ())
            motor.ChangeState ("AIFallingState");
    }
}
