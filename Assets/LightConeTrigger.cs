using UnityEngine;

// This script requires a MeshCollider to function. It will add one automatically.
[RequireComponent(typeof(MeshCollider))]
public class LightConeTrigger : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("The Light component this trigger should match.")]
    public Light spotlight;

    [Tooltip("The empty GameObject where the player will be sent.")]
    public Transform teleportTarget;

    [Tooltip("The tag assigned to your player GameObject.")]
    public string playerTag = "Player";

    private MeshCollider meshCollider;

    void Awake()
    {
        meshCollider = GetComponent<MeshCollider>();
        if (spotlight == null)
        {
            Debug.LogError("Spotlight not assigned on LightConeTrigger!");
            return;
        }

        // Generate a cone mesh that perfectly fits the spotlight's angle and range.
        GenerateConeMesh();
    }

    // This function is called by Unity when another collider enters our trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered is the player and if we have a target to send them to.
        if (other.CompareTag(playerTag) && teleportTarget != null)
        {
            Debug.Log("Player detected by spotlight! Teleporting...");
            // Move the player to the position of the teleport target.
            other.transform.position = teleportTarget.position;
        }
    }

    // This method creates a 3D cone mesh at runtime.
    private void GenerateConeMesh()
    {
        Mesh coneMesh = new Mesh();

        int numVertices = 18; // How detailed the base of the cone is.
        Vector3[] vertices = new Vector3[numVertices + 1];

        // The tip of the cone is at the light's origin.
        vertices[0] = Vector3.zero;

        float angle = spotlight.spotAngle * 0.5f * Mathf.Deg2Rad;
        float radius = spotlight.range * Mathf.Tan(angle);
        float forwardDistance = spotlight.range;

        // Create a circle of vertices for the base of the cone.
        for (int i = 0; i < numVertices; i++)
        {
            float radian = (2 * Mathf.PI * i) / (numVertices - 1);
            float x = radius * Mathf.Cos(radian);
            float y = radius * Mathf.Sin(radian);
            vertices[i + 1] = new Vector3(x, y, forwardDistance);
        }

        int[] triangles = new int[(numVertices - 1) * 3];
        // Create the triangle faces for the sides of the cone.
        for (int i = 0; i < numVertices - 1; i++)
        {
            triangles[i * 3] = 0;
            triangles[i * 3 + 1] = i + 1;
            triangles[i * 3 + 2] = i + 2;
        }

        coneMesh.vertices = vertices;
        coneMesh.triangles = triangles;
        coneMesh.RecalculateNormals();

        // Configure the MeshCollider to be a convex trigger.
        meshCollider.sharedMesh = coneMesh;
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }
}
