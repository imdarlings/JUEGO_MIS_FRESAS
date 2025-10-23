using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    private Animator animator;
    private bool enSuelo;

    [Header("Límites del mapa")]
    public float minX = -14f, maxX = 47f;
    public float minY = -7f, maxY = 37f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // --- Movimiento Horizontal (PC o móvil) ---
        float move = Input.GetAxis("Horizontal");

#if UNITY_ANDROID || UNITY_IOS
        // Si se juega en celular, usar botones virtuales (izquierda/derecha)
        if (Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.position.x < Screen.width / 2) move = -1f;
            else move = 1f;
        }
#endif

        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // --- Animaciones ---
        animator.SetFloat("MOVIMIENTO", Mathf.Abs(move)); // caminar/reposo
        if (move != 0)
        {
            float flip = Mathf.Sign(move);
            transform.localScale = new Vector3(flip * 7f, 7f, 1f);
        }

        // --- Saltar ---
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump")) && enSuelo)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetTrigger("SALTO");
        }

        // --- Límites del mapa ---
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar suelo
        if (collision.contacts[0].normal.y > 0.5f)
            enSuelo = true;
    }

    // Para animaciones especiales
    public void RecibirDaño()
    {
        animator.SetTrigger("DAÑO");
    }

    public void Morir()
    {
        animator.SetTrigger("MUERTE");
        rb.linearVelocity = Vector2.zero;
        enabled = false; // desactiva el control
    }
}
