using UnityEngine;

public class Player : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Win"))
        {
            GameManager.Instance.hasWon = true;
        }
    }
}
