using UnityEngine;

public class SimplePendulum : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.mass = 1f;  
        rb.angularDrag = 0.05f; 
    }

    void Update()
    {
        float angle = Vector3.Angle(transform.up, Vector3.down); 
        Debug.Log("Pendulum Angle: " + angle);

        // You can add other controls, e.g., applying forces if needed
    }
}
