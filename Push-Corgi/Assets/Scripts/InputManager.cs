using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private Vector3 currentPos;
    private bool isDragging;
    private Transform toDrag;
    private bool isClickedOn
    {
        get
        {
            Ray ray = Camera.main.ScreenPointToRay(currentPos);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player")
                    toDrag = hit.transform;

                return hit.transform.tag == "Player";
            }
            return false;
        }
    }

    private Vector3 worldPos
    {
        get
        {
            float z = Camera.main.ScreenToWorldPoint(transform.position).z;     //cambio
            return Camera.main.ScreenToWorldPoint(currentPos + new Vector3(0, 0, z));
        }
    }

    public void OnTap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isClickedOn)
            {
                StartCoroutine(Drag());
            }
        }

        if (context.canceled)
        {
            isDragging = false;
        }
    }

    public void OnScreenPos(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentPos = context.ReadValue<Vector2>();
        }
    }

    private IEnumerator Drag()
    {
        isDragging = true;
        // grab
        while (isDragging)
        {
            // dragging
            toDrag.position = new Vector3(toDrag.position.x, toDrag.position.y, worldPos.z);
            yield return null;
        }
        // drop
    }

}
