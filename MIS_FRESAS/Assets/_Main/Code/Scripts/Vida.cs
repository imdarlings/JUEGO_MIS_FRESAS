using UnityEngine;

public class Vida : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SumarVida(1); // suma 1 vida
            Destroy(gameObject);
        }
    }
}
