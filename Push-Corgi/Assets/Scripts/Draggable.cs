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
    private Camera mainCamera;
    private float distanceFromCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void StartDrag()
    {
        isDragging = true;
        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
    }

    public void StopDrag()
    {
        isDragging = false;
    }

    public void UpdateDragPosition(Vector2 screenPos)
    {
        if (!isDragging) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceFromCamera));

        if (direction == Direction.Horizontal)
        {
            transform.position = new Vector3(worldPos.x, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, worldPos.z);
        }
    }
}
