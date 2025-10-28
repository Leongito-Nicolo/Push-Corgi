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
            movement = StartCoroutine(Move());
            GameManager.Instance.hasWon = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            StopCoroutine(movement);
        }
    }
    public IEnumerator Move()
    {
        while (true)
        {
            // transform.position += Vector3.right * Time.deltaTime;
            yield return null;
        }
    }
}
