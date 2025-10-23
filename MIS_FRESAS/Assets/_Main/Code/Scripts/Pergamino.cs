using UnityEngine;

public class Pergamino : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.LeerPergamino();
            Destroy(gameObject);
        }
    }
}
