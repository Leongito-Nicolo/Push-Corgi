using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Draggable currentDraggable;
    private Vector2 currentPos;

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Ray ray = Camera.main.ScreenPointToRay(currentPos);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Draggable draggable;
                if (hit.transform.TryGetComponent(out draggable))
                {
                    Vector3 offset = hit.point - draggable.transform.position;
                    currentDraggable = draggable;
                    draggable.StartDrag(offset);
                }
                else if (hit.transform.parent?.TryGetComponent(out draggable) ?? false) //da levare
                {
                    Vector3 offset = hit.point - draggable.transform.position;

                    currentDraggable = draggable;
                    draggable.StartDrag(offset);
                }

                if (hit.transform.TryGetComponent(out Collectable collectable))
                {
                    collectable.CollectCoin();
                }
            }
        }

        if (context.canceled)
        {
            if (currentDraggable != null)
            {
                currentDraggable.StopDrag();
                currentDraggable = null;
            }
        }
    }

    public void OnPointerPosition(InputAction.CallbackContext context)
    {
        currentPos = context.ReadValue<Vector2>();

        if (currentDraggable != null)
        {
            currentDraggable.UpdateDragPosition(currentPos);
        }
    }
}
