using UnityEngine;

public class CubeCollision : MonoBehaviour
{
 public GameObject objectA; // Assign the first object in the Inspector
    public GameObject objectB; // Assign the second object in the Inspector
    public GameObject combinedObject; // Assign the new 3D object in the Inspector

    [Header("Magnetic Settings")]
    public float magneticForce = 10f; // Adjust this value for stronger/weaker attraction
    public float activationDistance = 5f; // Distance threshold for magnetic effect

    private bool isMagnetActive = false;

    private void FixedUpdate()
    {
        if (objectA.activeSelf && objectB.activeSelf)
        {
            Vector3 direction = objectB.transform.position - objectA.transform.position;
            float distance = direction.magnitude;

            if (distance < activationDistance)
            {
                isMagnetActive = true; // Activate magnetic effect
                // Apply magnetic force
                Vector3 force = direction.normalized * magneticForce / (distance * distance);
                objectA.GetComponent<Rigidbody>().AddForce(force);
                objectB.GetComponent<Rigidbody>().AddForce(-force);
            }
            else
            {
                isMagnetActive = false; // Deactivate magnetic effect
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject == objectA || other.gameObject == objectB) && isMagnetActive)
        {
            // Combine objects
            objectA.SetActive(false);
            objectB.SetActive(false);
            combinedObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((other.gameObject == objectA || other.gameObject == objectB) && isMagnetActive)
        {
            // Keep combined object visible
            combinedObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectA || other.gameObject == objectB)
        {
            // Separate objects and hide combined object if magnetic effect was active
            if (isMagnetActive)
            {
                combinedObject.SetActive(false);
                objectA.SetActive(true);
                objectB.SetActive(true);
            }
        }
    }
}
