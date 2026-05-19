using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class NeedleMovement : MonoBehaviour
{
    [Header("Movement")]
    [Tooltip("0 = initial position, positive = forward, negative = backward")]
    public float forwardOffset = 0f;

    [Header("Rotation")]
    [Tooltip("Pitch rotation in degrees (X axis)")]
    public float pitch = 0f;

    [Tooltip("Yaw rotation in degrees (Y axis)")]
    public float yaw = 0f;

    [Header("Slider movement")]
    public Slider movementSlider;  // Reference to the UI slider

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float direction;

    void Start()
    {
        // Store the starting transform so we can apply offsets relative to it
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        //Slider
        direction = movementSlider.value;

        // Apply initial movement
        movementSlider.onValueChanged.AddListener(
            direction => ApplyMovement(direction)
        );
    }

    private void ApplyMovement(float direction)
    {
        forwardOffset*=direction;
    }
        void Update()
    {
        // --- Movement along forward vector ---
        transform.position += transform.forward * forwardOffset * Time.deltaTime;

        // --- Rotation (pitch + yaw relative to initial rotation) ---
        Quaternion pitchRotation = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion yawRotation = Quaternion.AngleAxis(yaw, Vector3.up);

        // Combine rotations relative to the initial orientation
        //transform.rotation = initialRotation * yawRotation * pitchRotation;
    }
}
