using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector3 offset;
    private float zCoord;
    private bool isDragging = false;

    void OnMouseDown()
    {

        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        offset = transform.position - GetMouseWorldPosition();

        // Start dragging
        isDragging = true;
    }

    void OnMouseUp()
    {
        // Stop dragging
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {

            transform.position = GetMouseWorldPosition() + offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {

        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = zCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}
