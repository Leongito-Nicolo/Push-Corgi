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
                if (hit.transform.TryGetComponent(out Draggable draggable))
                {
                    currentDraggable = draggable;
                    draggable.StartDrag();
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
