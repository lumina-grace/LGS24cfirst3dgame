using UnityEngine;


[RequireComponent(typeof(SpotlightController))] // Ensures a SpotlightController is on the same GameObject
public class SpotlightAutomator : MonoBehaviour
{
    // Private reference to the controller script
    private SpotlightController controller;

    [Header("Automation Settings")]
    [Tooltip("How fast the light pans back and forth.")]
    public float panSpeed = 0.5f;

    [Tooltip("How fast the light tilts up and down.")]
    public float tiltSpeed = 0.3f;

    // We use private variables to track the movement's center and range
    private float panCenter;
    private float tiltCenter;
    private float panRadius;
    private float tiltRadius;

    // The 'Start' method is called once when the game begins
    void Start()
    {
        // Get the controller component from this GameObject
        controller = GetComponent<SpotlightController>();

        // Calculate the center and radius (size) of the movement based on the limits
        // set in the SpotlightController. This makes the automation respect your min/max angles.
        panCenter = (controller.maxPanAngle + controller.minPanAngle) / 2f;
        panRadius = (controller.maxPanAngle - controller.minPanAngle) / 2f;

        tiltCenter = (controller.maxTiltAngle + controller.minTiltAngle) / 2f;
        tiltRadius = (controller.maxTiltAngle - controller.minTiltAngle) / 2f;
    }

    // 'Update' is called once per frame
    void Update()
    {
        // We need to make sure the controller exists before we try to use it
        if (controller != null)
        {
            // Calculate the new angles using sine and cosine. Using Time.time creates a continuous, smooth wave.
            // This creates a smooth, looping circular or oval motion.
            float newPan = panCenter + Mathf.Cos(Time.time * panSpeed) * panRadius;
            float newTilt = tiltCenter + Mathf.Sin(Time.time * tiltSpeed) * tiltRadius;

            // Set the public panAngle and tiltAngle variables on the SpotlightController.
            // The controller's own Update method will then handle the actual rotation.
            controller.panAngle = newPan;
            controller.tiltAngle = newTilt;
        }
    }
}