using UnityEngine;

public class Trampa : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.RestarVida();
            Destroy(gameObject);
        }
    }
}
