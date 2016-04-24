using UnityEngine;
using System.Collections;

public class AIMotor : BaseMotor {

    private Vector3 destination = Vector3.zero;

    protected override void Start ()
    {
        base.Start ();

        // Put walking state on player and then create it
        state = gameObject.AddComponent<AIWalkingState> ();
        state.Construct ();
    }

    protected override void UpdateMotor ()
    {
        // Get the input
        MoveVector = Direction ();

        // Send the input to a filter
        MoveVector = state.ProcessMotion (MoveVector);
        RotationQuaternion = state.ProcessRotation (MoveVector);

        // Check if we need to change states
        state.Transition ();

        // Move
        Move ();
        Rotate ();
    }

    public void SetDestination (Transform t)
    {
        destination = t.position;
    }

    public Vector3 Direction ()
    {
        if (destination == Vector3.zero)
            return destination;

        Vector3 direction = destination - thisTransform.position;
        direction.Set (direction.x, 0, direction.z);

        return direction.normalized;
    }
}
