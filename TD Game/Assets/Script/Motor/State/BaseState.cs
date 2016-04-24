using UnityEngine;
using System.Collections;

public abstract class BaseState : MonoBehaviour {

    protected BaseMotor motor;

    // Declare region for state machine
    #region baseState implementation

    // Called to construct state
    public virtual void Construct ()
    {
        motor = GetComponent<BaseMotor> ();
    }

    // Called to destroy state
    public virtual void Destruct ()
    {
        Destroy (this);
    }

    // Called every frame to check if we need to change our current state
    public virtual void Transition ()
    {

    }

    #endregion

    public abstract Vector3 ProcessMotion (Vector3 input);
    public virtual Quaternion ProcessRotation (Vector3 input)
    {
        return transform.rotation;
    }

    protected void ApplySpeed (ref Vector3 input, float speed)
    {
        input *= speed;
    }

    protected void ApplyGravity (ref Vector3 input, float gravity)
    {
        motor.VerticalVelocity -= gravity * Time.deltaTime;

        // Clamp our value
        motor.VerticalVelocity = Mathf.Clamp (motor.VerticalVelocity, -motor.TerminalVelocty, motor.TerminalVelocty);

        input.Set (input.x, motor.VerticalVelocity, input.z);
    }
}
