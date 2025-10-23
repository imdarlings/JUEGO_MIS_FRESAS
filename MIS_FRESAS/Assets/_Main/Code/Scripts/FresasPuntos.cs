using UnityEngine;

public class FresasPuntos : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SumarFresa();
            Destroy(gameObject);
        }
    }
}
