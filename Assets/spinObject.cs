using UnityEngine;

/// <summary>
/// This script continuously rotates the GameObject it is attached to
/// and also makes it bounce up and down slightly.
/// The speed and height of the motion can be configured in the Unity Inspector.
/// </summary>
public class spinObject : MonoBehaviour
{
    // --- FIELDS ---

    [Header("Rotation Speed")]
    [Tooltip("The speed of rotation around the X-axis in degrees per second.")]
    public float rotationSpeedX = 0f;

    [Tooltip("The speed of rotation around the Y-axis in degrees per second.")]
    public float rotationSpeedY = 50.0f;

    [Tooltip("The speed of rotation around the Z-axis in degrees per second.")]
    public float rotationSpeedZ = 0f;

    [Header("Bounce Settings")]
    [Tooltip("How high the object will bounce from its starting position.")]
    public float bounceHeight = 0.25f;

    [Tooltip("How fast the object will bounce up and down.")]
    public float bounceSpeed = 1.0f;

    // A private variable to store the object's initial position.
    private Vector3 startPosition;


    // --- UNITY METHODS ---

    /// <summary>
    /// Start is called before the first frame update.
    /// We use it to record the object's starting position.
    /// </summary>
    void Start()
    {
        // Store the initial position of the object.
        // The bounce will be calculated relative to this point.
        startPosition = transform.position;
    }

    /// <summary>
    /// Update is called once per frame.
    /// We use it to apply both the rotation and the bounce motion.
    /// </summary>
    void Update()
    {
        // --- Rotation Logic ---
        // Calculate the rotation for this frame based on the speed and time.
        float rotationX = rotationSpeedX * Time.deltaTime;
        float rotationY = rotationSpeedY * Time.deltaTime;
        float rotationZ = rotationSpeedZ * Time.deltaTime;

        // Apply the rotation to the object.
        transform.Rotate(rotationX, rotationY, rotationZ, Space.World);


        // --- Bouncing Logic ---
        // Calculate the new Y position using a sine wave.
        // This creates a smooth, oscillating up-and-down motion.
        float newY = startPosition.y + bounceHeight * Mathf.Sin(Time.time * bounceSpeed);

        // Apply the new position. We only change the Y coordinate,
        // keeping the original X and Z from its starting position.
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}

