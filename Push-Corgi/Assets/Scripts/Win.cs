using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private Canvas canva;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            canva.gameObject.SetActive(true);
        }
    }
}
