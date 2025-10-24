using System.Collections;
using UnityEngine;

public enum Direction
{
    Vertical,
    Horizontal
}

public enum Size
{
    Even,
    Odd
}

[RequireComponent(typeof(BoxCollider))]
public class Draggable : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private Size size;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float checkOffset = 0.05f;
    [SerializeField] private LayerMask obstacleMask;

    private Camera mainCamera;
    private float distanceFromCamera;
    private Vector3 offset;
    private Vector3 dragPos;
    private bool isDragging;



    private BoxCollider col;

    private void Awake()
    {
        mainCamera = Camera.main;
        col = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!isDragging) return;

        Vector3 currentPos = transform.position;
        Vector3 targetPos = dragPos;
        targetPos.y = currentPos.y;

        Vector3 moveDir;
        float distance;

        if (direction == Direction.Horizontal)
        {
            targetPos.z = currentPos.z;
            moveDir = Vector3.right * Mathf.Sign(targetPos.x - currentPos.x);
            distance = Mathf.Abs(targetPos.x - currentPos.x);
        }
        else
        {
            targetPos.x = currentPos.x;
            moveDir = Vector3.forward * Mathf.Sign(targetPos.z - currentPos.z);
            distance = Mathf.Abs(targetPos.z - currentPos.z);
        }

        if (distance < 0.01f)
            return;

        if (Physics.BoxCast(col.bounds.center, col.bounds.extents * 0.9f, moveDir, out RaycastHit hit, Quaternion.identity, distance, obstacleMask))
        {
            Vector3 stopPos = currentPos;
            if (direction == Direction.Horizontal)
                stopPos.x = hit.point.x - moveDir.x * (col.bounds.extents.x + checkOffset);
            else
                stopPos.z = hit.point.z - moveDir.z * (col.bounds.extents.z + checkOffset);

            transform.position = Vector3.Lerp(currentPos, stopPos, Time.deltaTime * moveSpeed);
        }
        else
        {
            Vector3 newPos = Vector3.Lerp(currentPos, targetPos, Time.deltaTime * moveSpeed);
            transform.position = newPos;
        }
    }

    public void StartDrag(Vector3 offset)
    {
        isDragging = true;
        distanceFromCamera = Vector3.Distance(mainCamera.transform.position, transform.position);
        this.offset = offset;
    }

    public void StopDrag()
    {
        GameManager.Instance.movesCounter++;
        isDragging = false;
        offset = Vector3.zero;
        SnapToGrid();
    }

    public void UpdateDragPosition(Vector2 screenPos)
    {
        if (!isDragging) return;

        Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceFromCamera));
        Vector3 targetPos = transform.position;

        if (direction == Direction.Horizontal)
            targetPos.x = worldPos.x - offset.x;
        else
            targetPos.z = worldPos.z - offset.z;

        dragPos = targetPos;
    }

    private void SnapToGrid()
    {
        Vector3 pos = transform.position;

        if (size == Size.Even)
        {
            if (direction == Direction.Horizontal)
                pos.x = Mathf.Round(pos.x);
            else
                pos.z = Mathf.Round(pos.z);
        }
        else
        {
            if (direction == Direction.Horizontal)
                pos.x = Mathf.Round(pos.x - 0.5f) + 0.5f;
            else
                pos.z = Mathf.Round(pos.z - 0.5f) + 0.5f;
        }

        StartCoroutine(SnapRoutine(pos));
    }

    private IEnumerator SnapRoutine(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * 15);
            yield return null;
        }
        transform.position = target;
    }
}
