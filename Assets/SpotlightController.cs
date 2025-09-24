using UnityEngine;

[ExecuteInEditMode] // Allows the script to run in the editor, so you see changes immediately.
public class SpotlightController : MonoBehaviour
{
    // Public variables to link our GameObjects in the Inspector
    [Header("Rig Components")]
    public Transform panPivot;
    public Transform tiltPivot;
    public Light spotlight; // Assign your "LightEmitter" light here

    // Sliders to control the angles in the Inspector
    [Header("Movement Control")]
    [Range(-180f, 180f)]
    public float panAngle = 0f;

    [Range(-90f, 90f)]
    public float tiltAngle = 0f;

    // New variables to define the rotation limits
    [Header("Rotation Limits")]
    [Range(-180f, 180f)]
    public float minPanAngle = -90f;

    [Range(-180f, 180f)]
    public float maxPanAngle = 90f;

    [Range(-90f, 90f)]
    public float minTiltAngle = -45f;

    [Range(-90f, 90f)]
    public float maxTiltAngle = 80f;

    // New variables for light control
    [Header("Light Control")]
    public Color lightColor = Color.white;
    [Range(0f, 8f)]
    public float lightIntensity = 1f;


    void Update()
    {
        // Clamp the input angles to stay within the defined limits
        panAngle = Mathf.Clamp(panAngle, minPanAngle, maxPanAngle);
        tiltAngle = Mathf.Clamp(tiltAngle, minTiltAngle, maxTiltAngle);

        // --- Update Rotation ---
        // Ensure we have assigned the pivots before trying to use them
        if (panPivot != null)
        {
            // The Pan pivot rotates around the Y-axis (left and right)
            panPivot.localRotation = Quaternion.Euler(0f, panAngle, 0f);
        }
        if (tiltPivot != null)
        {
            // The Tilt pivot rotates around the X-axis (up and down)
            tiltPivot.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f);
        }

        // --- Update Light Properties ---
        // Ensure we have assigned the light before trying to change it
        if (spotlight != null)
        {
            spotlight.color = lightColor;
            spotlight.intensity = lightIntensity;
        }
    }
}

