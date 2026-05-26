using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static UnityEngine.LowLevelPhysics2D.PhysicsBody;

public class NeedleMovement : MonoBehaviour
{
    [Header("Initial position")]
    [Tooltip("Starting position: L3/L4 or L4/L5")]
    public Vector3 initialPositionA = new Vector3(-0.0162f, 0.0185f, 0.1875f); //L3 L4 space
    public Vector3 initialPositionB = new Vector3(-0.0162f,-0.0034f, 0.187f); //L4 L5 space
    public Vector3 initialLocalPosition; // Remembers where you placed the needle in the Editor, relative to its parent.
    private Transform parentTransform;

    [Header("Toggle Button")]
    public Toggle spaceToggle;   // Assign in Inspector
    public bool space = false;

    [Header("Movement Range")]
    [Tooltip("Minimum local offset from the initial position")]
    public float minDistance = 0f;
    [Tooltip("Maximum local offset from the initial position")]
    public float maxDistance = 0.1f;

    [Header("Smooth Motion Settings")]
    [Tooltip("Time it takes to reach the target offset (seconds)")]
    public float smoothTime = 0.3f;
    
    // Separate velocities for each SmoothDamp tracker
    private float positionVelocity = 0f;
    private float pitchVelocity = 0f;
    private float yawVelocity = 0f;
    private float rollVelocity = 0f;

    [Header("Rotation")]
    [Tooltip("Pitch rotation in degrees (X axis)")]
    public float pitch = 0f;
    [Tooltip("Yaw rotation in degrees (Y axis)")]
    public float yaw = 0f;
    [Tooltip("Roll rotation in degrees (Z axis)")]
    public float roll = 0f;
    [Tooltip("GameObject that defines the pivot for rotation")]
    public GameObject pivotObject;
    public bool rot = false;

    private float currentPitch;
    private float currentYaw;
    private float currentRoll;

    private Quaternion initialRotation;
    private Quaternion initialLocalRotationToPivot;
    private Vector3 initialLocalPositionToPivot;

    [Header("Slider movement")]
    public Slider movementSlider;  // Reference to the UI slider

    private float currentOffset;      // current distance from initial position  


    void Start()
    {
        // Store local position relative to parent
        //initialLocalPosition = transform.localPosition;
        initialLocalPosition = initialPositionA;
        parentTransform = transform.parent; // keep reference to parent
        initialRotation = transform.rotation; //world coordinates

        if (pivotObject != null)
        {
            /// We still cache the local rotation offset relative to the pivot orientation
            initialLocalRotationToPivot = Quaternion.Inverse(pivotObject.transform.rotation) * transform.rotation;
        }

        if (spaceToggle != null)
            spaceToggle.onValueChanged.AddListener(OnSpaceToggleChanged);
    }

    void Update()
    {
        //Read slider movement
        float t = movementSlider.value;
        //Adjust the value to the interval
        float targetOffset = Mathf.Lerp(minDistance, maxDistance, t);

        // Smoothly move toward target offset
        currentOffset = Mathf.SmoothDamp(currentOffset, targetOffset, ref positionVelocity, smoothTime);

        // Convert local starting point into world space
        Vector3 worldStart = parentTransform.TransformPoint(initialLocalPosition); //converts that local position into world coordinates at runtime.
        

        // --- Rotation & Pivot Math ---
        if (rot && pivotObject != null)
        {
            // Smoothly move each angle toward its target using independent velocity variables
            currentPitch = Mathf.SmoothDamp(currentPitch, pitch, ref pitchVelocity, smoothTime);
            currentYaw = Mathf.SmoothDamp(currentYaw, yaw, ref yawVelocity, smoothTime);
            currentRoll = Mathf.SmoothDamp(currentRoll, roll, ref rollVelocity, smoothTime);

            // Construct the incremental rotation modifier
            Quaternion incrementalRotation = Quaternion.Euler(currentPitch, currentYaw, currentRoll);

            // Determine what the pivot's modified world orientation looks like
            Quaternion targetPivotRotation = pivotObject.transform.rotation * incrementalRotation;

            // Update the needle's rotation based on the pivot's new rotation
            transform.rotation = targetPivotRotation * initialLocalRotationToPivot;

            // Calculate the local offset relative to the pivot USING THE CORRECT starting position
            Vector3 dynamicLocalPosToPivot = pivotObject.transform.InverseTransformPoint(worldStart);

            // Calculate where that precise starting point lands when swung around the pivot
            Vector3 rotatedPivotOffset = targetPivotRotation * dynamicLocalPosToPivot;
            Vector3 rotatedWorldStart = pivotObject.transform.position + rotatedPivotOffset;

            // 3. Apply position: Rotated starting position + forward push
            transform.position = rotatedWorldStart + transform.forward * currentOffset;
        }
        else
        {
            // If rotation is turned off, match standard initial rotation and push along standard forward path
            //transform.rotation = initialRotation;
            // Fallback if rotation is turned off
            // move along normal standard forward path
            transform.position = worldStart + transform.forward * currentOffset; //ensures movement follows the object’s facing direction in world space.
        }

    }

    // Call this method to toggle between A and B
    private void ToggleInitialPosition(bool sp)
    {
        // If sp is true, use position A. If false, use position B.
        initialLocalPosition = sp ? initialPositionB : initialPositionA;

        // Recalculate local tracking vector to pivot when base position shifts
        if (pivotObject != null)
        {
            Vector3 worldStart = parentTransform.TransformPoint(initialLocalPosition);
            initialLocalPositionToPivot = pivotObject.transform.InverseTransformPoint(worldStart);
        }

    }

    private void OnSpaceToggleChanged(bool isOn)
    {
        // Update the space variable
        space = isOn;
        ToggleInitialPosition(space);
    }
}

