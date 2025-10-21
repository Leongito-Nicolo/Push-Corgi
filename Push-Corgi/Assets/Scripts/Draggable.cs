using UnityEditor;
using UnityEngine;

public enum Direction
{
    Vertical,
    Horizontal
}

public class Draggable : MonoBehaviour
{
    [SerializeField] private Direction direction;
    private bool isDragging = false;
    private bool canDrag = true;
    private Camera mainCamera;
    private float distanceFromCamera;
    private Vector3 offset;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void StartDrag(Vector3 offset)
    {
        isDragging = true;
        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
        this.offset = offset;
    }

    public void StopDrag()
    {
        offset = Vector2.zero;
        isDragging = false;
        FindNearestPoint();
    }

    public void UpdateDragPosition(Vector2 screenPos)
    {
        if (!isDragging) return;
        if (!canDrag) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceFromCamera));
        if (direction == Direction.Horizontal)
        {
            transform.position = new Vector3(worldPos.x - offset.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, worldPos.z - offset.z);
        }
    }

    public void FindNearestPoint()
    {
        if (direction == Direction.Horizontal)
        {
            transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("x");
        if (collision.transform.tag == "Obstacle")
        {
            Debug.Log("a");
            canDrag = false;
        }
    }

}
