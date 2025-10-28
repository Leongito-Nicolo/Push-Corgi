using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Coroutine movement;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            Draggable drag = transform.GetComponent<Draggable>();
            //StopAllCoroutines();
            StartCoroutine(drag.SnapRoutine(Vector3.right * 12f));
            GameManager.Instance.hasWon = true;
        }
    }
}
