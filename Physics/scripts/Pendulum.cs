using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public Transform pivot;         // The pivot point
    public float length = 5f;       // Length of the pendulum
    public float gravity = 9.81f;   // Gravitational constant
    public float damping = 0.01f;   // Damping to simulate friction
    public float initialAngle = 30f;// Initial angle in degrees

    private float angularVelocity = 0f;
    private float angle;            // Current angle in radians

    void Start()
    {
        // Convert initial angle to radians
        angle = initialAngle * Mathf.Deg2Rad;
    }

    void Update()
    {
        // Pendulum Physics Equation: angular acceleration = -(g / length) * sin(theta)
        float angularAcceleration = -(gravity / length) * Mathf.Sin(angle);

        // Apply damping to angular velocity
        angularVelocity += angularAcceleration * Time.deltaTime;
        angularVelocity *= (1 - damping); // Simulate air resistance or friction

        // Update the angle based on the angular velocity
        angle += angularVelocity * Time.deltaTime;

        // Convert angle to rotation for the pendulum
        pivot.localRotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);
    }
}
