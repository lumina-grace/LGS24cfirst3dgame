using UnityEngine;

// Requires these components to draw the mesh
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class VolumetricLightBeam : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("The Light component this beam should match.")]
    public Light spotlight;

    [Tooltip("The material to use for the beam. This should be a transparent/additive material.")]
    public Material beamMaterial;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();

        if (spotlight == null)
        {
            Debug.LogError("Spotlight not assigned on VolumetricLightBeam!");
            enabled = false; // Disable script if no light is assigned
            return;
        }

        if (beamMaterial == null)
        {
            Debug.LogError("Beam Material not assigned on VolumetricLightBeam!");
            enabled = false; // Disable script if no material is assigned
            return;
        }

        meshRenderer.material = beamMaterial;

        // Generate the cone mesh
        GenerateConeMesh();
    }

    // This is useful if you want to be able to adjust the light in the editor and see the beam update.
    void OnValidate()
    {
        // OnValidate is called when the script is loaded or a value is changed in the Inspector.
        // We need to make sure we have the components before trying to use them.
        if (meshFilter == null) meshFilter = GetComponent<MeshFilter>();
        if (meshRenderer == null) meshRenderer = GetComponent<MeshRenderer>();
        if (spotlight != null)
        {
            GenerateConeMesh();
        }
    }

    private void GenerateConeMesh()
    {
        Mesh coneMesh = new Mesh();
        coneMesh.name = "VolumetricLightCone";

        int numVertices = 32; // More detail for a visible mesh
        Vector3[] vertices = new Vector3[numVertices + 1];

        vertices[0] = Vector3.zero; // Tip of the cone

        float angle = spotlight.spotAngle * 0.5f * Mathf.Deg2Rad;
        float radius = spotlight.range * Mathf.Tan(angle);
        float forwardDistance = spotlight.range;

        // Create a circle of vertices for the base of the cone
        for (int i = 0; i < numVertices; i++)
        {
            float radian = (2 * Mathf.PI * i) / (numVertices - 1);
            float x = radius * Mathf.Cos(radian);
            float y = radius * Mathf.Sin(radian);
            vertices[i + 1] = new Vector3(x, y, forwardDistance);
        }

        int[] triangles = new int[(numVertices - 1) * 3];
        // Create the triangle faces for the sides of the cone
        for (int i = 0; i < numVertices - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        coneMesh.vertices = vertices;
        coneMesh.triangles = triangles;
        coneMesh.RecalculateNormals();

        // Assign the generated mesh to the MeshFilter to be rendered.
        meshFilter.mesh = coneMesh;
    }
}
