using UnityEngine;

public class MagneticForce : MonoBehaviour
{
    public Transform targetObject; // Object2
    public GameObject thirdObject; // Third object that appears on contact

    private Vector3 originalPositionThis;
    private Vector3 originalPositionTarget;

    public float forceMultiplier = 5.0f; // Reduced force to prevent swapping
    public float minDistance = 1.0f; // Minimum distance to stop force application
    public float stoppingDistance = 0.5f; // Stopping distance to prevent objects from overshooting

    private Rigidbody thisRb;
    private Rigidbody targetRb;

    private bool isInContact = false;
    private bool shouldApplyForce = false;

    void Start()
    {
        // Cache Rigidbody components
        thisRb = GetComponent<Rigidbody>();
        targetRb = targetObject.GetComponent<Rigidbody>();

        // Store the original positions
        originalPositionThis = transform.position;
        originalPositionTarget = targetObject.position;

        // Initially hide the third object
        thirdObject.SetActive(false);

        // Disable gravity for smoother movement
        thisRb.useGravity = false;
        targetRb.useGravity = false;
    }

    // Detect when Object2 enters the magnetic trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == targetObject)
        {
            shouldApplyForce = true; // Start applying force
        }
    }

    // Detect when Object2 exits the magnetic trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.transform == targetObject)
        {
            shouldApplyForce = false; // Stop applying force
            thirdObject.SetActive(false); // Hide third object
            ResetPositions(); // Reset positions
        }
    }

    // Detect when the objects collide
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == targetObject)
        {
            isInContact = true; // Set contact flag
            shouldApplyForce = false; // Stop applying force

            // Show the third object on contact
            thirdObject.SetActive(true);

            // Freeze both objects to prevent sliding
            thisRb.constraints = RigidbodyConstraints.FreezeAll;
            targetRb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Detect when the objects separate after collision
    void OnCollisionExit(Collision collision)
    {
        if (collision.transform == targetObject)
        {
            isInContact = false;
            thirdObject.SetActive(false); // Hide third object

            // Unfreeze the objects
            thisRb.constraints = RigidbodyConstraints.None;
            targetRb.constraints = RigidbodyConstraints.None;

            // Reset positions
            ResetPositions();
        }
    }

    void FixedUpdate()
    {
        // Only apply force if objects are not in contact and should apply force
        if (shouldApplyForce && !isInContact)
        {
            // Calculate the direction between the objects
            Vector3 direction = targetObject.position - transform.position;
            float distance = direction.magnitude;

            // Apply magnetic force only if objects are beyond stopping distance
            if (distance > stoppingDistance)
            {
                // Smoothly move the objects towards each other
                Vector3 newPositionThis = Vector3.MoveTowards(transform.position, targetObject.position, forceMultiplier * Time.fixedDeltaTime);
                Vector3 newPositionTarget = Vector3.MoveTowards(targetObject.position, transform.position, forceMultiplier * Time.fixedDeltaTime);

                // Set the new positions
                thisRb.MovePosition(newPositionThis);
                targetRb.MovePosition(newPositionTarget);
            }
        }
    }

    // Reset the positions and velocities of both objects
    private void ResetPositions()
    {
        transform.position = originalPositionThis;
        targetObject.position = originalPositionTarget;

        // Reset velocities
        thisRb.velocity = Vector3.zero;
        thisRb.angularVelocity = Vector3.zero;
        targetRb.velocity = Vector3.zero;
        targetRb.angularVelocity = Vector3.zero;
    }
}
