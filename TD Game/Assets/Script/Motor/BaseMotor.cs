using UnityEngine;
using System.Collections;

public abstract class BaseMotor : MonoBehaviour {

    // Create stuff
    protected CharacterController controller;
    protected BaseState state;
    protected Transform thisTransform;

    // Set the base variable values
    private float baseSpeed = 5.0f;
    private float baseGravity = 25.0f;
    private float baseJumpForce = 7.0f;
    private float terminalVelocity = 30.0f;
    private float groundRayDistance = 0.5f;
    private float groundRayInnerOffset = 0.1f;

    public float Speed
    {
        get
        {
            return baseSpeed;
        }
    }

    public float Gravity
    {
        get
        {
            return baseGravity;
        }
    }

    public float JumpForce
    {
        get
        {
            return baseJumpForce;
        }
    }

    public float TerminalVelocty
    {
        get
        {
            return terminalVelocity;
        }
    }

    public float VerticalVelocity
    {
        set;
        get;
    }

    public Vector3 MoveVector
    {
        set;
        get;
    }

    public Quaternion RotationQuaternion
    {
        set;
        get;
    }

    protected abstract void UpdateMotor ();

    protected virtual void Start ()
    {
        controller = gameObject.AddComponent<CharacterController> ();
        thisTransform = transform;
    }

    private void Update ()
    {
        UpdateMotor ();
    }

    // Be able to clearly read UpdateMotor
    protected virtual void Move ()
    {
        controller.Move (MoveVector * Time.deltaTime);
    }

    protected virtual void Rotate ()
    {
        thisTransform.rotation = RotationQuaternion;
    }

    public void ChangeState (string stateName)
    {
        System.Type type = System.Type.GetType (stateName);

        state.Destruct ();
        state = gameObject.AddComponent (type) as BaseState;
        state.Construct ();
    }

    // Check if they are on the ground
    public virtual bool Grounded ()
    {
        RaycastHit hit;
        Vector3 ray;

        // Y value of every array
        float yRay = (controller.bounds.center.y - controller.bounds.extents.y) + 0.3f,
            centerX = controller.bounds.center.x,
            centerZ = controller.bounds.center.z,
            extentX = controller.bounds.extents.z - groundRayInnerOffset,
            extentZ = controller.bounds.extents.z - groundRayInnerOffset;


        // Middle Raycast
        ray = new Vector3 (centerX, yRay, centerZ);
        Debug.DrawRay (ray, Vector3.down, Color.green);

        if (Physics.Raycast (ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }

        ray = new Vector3 (centerX - extentX, yRay, centerZ + extentZ);
        Debug.DrawRay (ray, Vector3.down, Color.green);

        if (Physics.Raycast (ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }

        ray = new Vector3 (centerX - extentX, yRay, centerZ - extentZ);
        Debug.DrawRay (ray, Vector3.down, Color.green);

        if (Physics.Raycast (ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }

        ray = new Vector3 (centerX - extentX, yRay, centerZ - extentZ);
        Debug.DrawRay (ray, Vector3.down, Color.green);

        if (Physics.Raycast (ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }

        ray = new Vector3 (centerX + extentX, yRay, centerZ - extentZ);
        Debug.DrawRay (ray, Vector3.down, Color.green);

        if (Physics.Raycast (ray, Vector3.down, out hit, groundRayDistance))
        {
            return true;
        }

        return false;
    }
}
