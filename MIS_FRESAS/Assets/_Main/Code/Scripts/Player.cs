using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpForce = 15f;
    public int maxSaltos = 5;

    private int saltosRestantes;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Suelo")]
    public Transform groundCheck;
    public float groundRadius = 0.15f;
    public LayerMask capaSuelo;
    private bool enSuelo;

    [Header("Limites del mapa")]
    public float minX = -14f, maxX = 47f;
    public float minY = -7f, maxY = 37f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        saltosRestantes = maxSaltos;
    }

    void Update()
    {
        // DETECTAR SUELO
        bool estabaEnSuelo = enSuelo;
        enSuelo = Physics2D.OverlapCircle(groundCheck.position, groundRadius, capaSuelo);

        // Si aterrizi = reinicia saltos
        if (enSuelo && !estabaEnSuelo)
        {
            saltosRestantes = maxSaltos;
        }

        // MOVIMIENTO HORIZONTAL
        float move = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        // FLIP
        if (move != 0)
            transform.localScale = new Vector3(Mathf.Sign(move) * 7f, 7f, 1f);

        // SALTO 
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes > 0)
        {
            anim.SetTrigger("SALTO");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            saltosRestantes--;
        }



        // ANIMACIONES
        anim.SetBool("ENSUELO", enSuelo);
        anim.SetFloat("MOVIMIENTO", Mathf.Abs(move));

        // LIMITES DEL MAPA
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }

   

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
