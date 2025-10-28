using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float velocity = 5f;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            Draggable drag = transform.GetComponent<Draggable>();
            StartCoroutine(drag.SnapRoutine(Vector3.right * velocity));
            GameManager.Instance.hasWon = true;
        }
    }
}
