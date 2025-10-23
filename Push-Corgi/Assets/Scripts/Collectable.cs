using UnityEngine;

public class Collectable : MonoBehaviour
{
    public void CollectCoin()
    {
        Destroy(gameObject);
    }
}
