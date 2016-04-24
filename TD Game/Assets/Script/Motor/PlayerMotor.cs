using UnityEngine;
using System.Collections;

public class PlayerMotor : BaseMotor {

    private CameraMotor camMotor;
    private Transform camTransform;

    protected override void Start ()
    {
        base.Start ();

        // Put walking state on player and then create it
        state = gameObject.AddComponent<WalkingState> ();
        state.Construct ();

        camMotor = gameObject.AddComponent<CameraMotor> ();
        camMotor.Init ();
        camTransform = camMotor.CameraContainer;
    }

    protected override void UpdateMotor ()
    {
        // Get the input
        MoveVector = InputDirection ();

        // Rotate our MoveVector with Camera forward
        MoveVector = RotateWithView (MoveVector);

        // Send the input to a filter
        MoveVector = state.ProcessMotion (MoveVector);
        RotationQuaternion = state.ProcessRotation (MoveVector);

        // Check if we need to change states
        state.Transition();

        // Move
        Move ();
        Rotate ();
    }

    // Gets the input from keyboard
    private Vector3 InputDirection ()
    {
        // Clean direction vector
        Vector3 direction = Vector3.zero;

        // Get the inputs for direction
        direction.x = Input.GetAxis ("Horizontal");
        direction.z = Input.GetAxis ("Vertical");

        if (direction.magnitude > 1)
            direction.Normalize ();

        return direction;
    }

    private Vector3 RotateWithView (Vector3 input)
    {
        Vector3 direction = camTransform.TransformDirection (input);
        direction.Set (direction.x, 0, direction.z);

        return direction.normalized * input.magnitude;
    }
}
