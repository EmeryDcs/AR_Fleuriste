using UnityEngine;

public class RotateOnAxis : MonoBehaviour
{
    [Tooltip("Rotation speed in degrees per second")]
    public float rotationSpeed = 10f;

    [Tooltip("Rotation axis (1 for active, 0 for inactive)")]
    public Vector3 rotationAxis = new Vector3(0, 1, 0);

    private Vector3 normalizedAxis;

    void Start()
    {
        normalizedAxis = rotationAxis.normalized;
    }

    void Update()
    {
        transform.Rotate(normalizedAxis * rotationSpeed * Time.deltaTime, Space.World);
    }
}
