using UnityEngine;

public class Tiempo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SumarTiempo(10f);
            Destroy(gameObject);
        }
    }
}
