using UnityEngine;

/// <summary>
/// This script makes a Light component on a GameObject fade through the RGB color spectrum over time.
/// It requires a Light component to be attached to the same GameObject.
/// </summary>
[RequireComponent(typeof(Light))]
public class LightFader : MonoBehaviour
{
    // --- FIELDS ---

    [Header("Light Settings")]
    [Tooltip("Controls how fast the light cycles through colors.")]
    public float fadeSpeed = 1.0f;

    // A private variable to hold a reference to the Light component.
    private Light lightComponent;


    // --- UNITY METHODS ---

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// It's used here to get the Light component.
    /// </summary>
    void Awake()
    {
        // Get the Light component attached to this GameObject.
        // The [RequireComponent] attribute ensures this will never be null.
        lightComponent = GetComponent<Light>();
    }

    /// <summary>
    /// Update is called once per frame.
    /// We use it to calculate and apply the new color to the light.
    /// </summary>
    void Update()
    {
        // We use sine waves with different phase shifts for each color channel (R, G, B).
        // This creates a smooth and continuous transition through the color spectrum.
        // Time.time provides a constantly increasing value, perfect for driving the sine function.
        // The result of Sin is between -1 and 1, so we add 1 and divide by 2 to map it to the 0-1 range required for color values.

        // Calculate the red component.
        float red = (Mathf.Sin(Time.time * fadeSpeed) + 1) / 2;

        // Calculate the green component, with a phase shift to desynchronize it from red.
        float green = (Mathf.Sin(Time.time * fadeSpeed + 2) + 1) / 2;

        // Calculate the blue component, with another phase shift.
        float blue = (Mathf.Sin(Time.time * fadeSpeed + 4) + 1) / 2;

        // Create a new Color with the calculated RGB values and apply it to the light.
        lightComponent.color = new Color(red, green, blue);
    }
}
