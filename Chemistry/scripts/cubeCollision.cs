using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    // Reference to the new GameObject component to be added on collision
    public GameObject newComponentPrefab;

    // This function is called when the object collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Print a message when collision occurs
        Debug.Log("Collision occurred between " + gameObject.name + " and " + collision.gameObject.name);

        // Deactivate both cubes
        gameObject.SetActive(false);
        collision.gameObject.SetActive(false);

        // Instantiate the new component at the position of the first cube
        if (newComponentPrefab != null)
        {
            Instantiate(newComponentPrefab, transform.position, Quaternion.identity);
            Debug.Log("New component added.");
        }
        else
        {
            Debug.LogWarning("No new component prefab assigned.");
        }
    }
}
