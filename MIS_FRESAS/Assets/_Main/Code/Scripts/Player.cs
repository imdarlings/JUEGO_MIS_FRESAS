using JetBrains.Annotations;
using System;
using System.Collections;
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
    private int vidas;


    [Header("Limites del mapa")]
    public float minX = -14f, maxX = 47f;
    public float minY = -7f, maxY = 37f;
    private float move;

    [Header("Animación daño permanente")]
    public string dañoStateName = ""; 
    private int dañoStateHash = 0;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (anim == null) anim = GetComponentInChildren<Animator>();

        if (!string.IsNullOrEmpty(dañoStateName))
            dañoStateHash = Animator.StringToHash(dañoStateName);

        saltosRestantes = maxSaltos;
    }

    void Update()
    {
        if (isDead) return; // bloquea controles si está "muerto"

        // DETECTAR SUELO
        bool estabaEnSuelo = enSuelo;
        enSuelo = Physics2D.OverlapCircle(groundCheck.position, groundRadius, capaSuelo);

        // Si aterriza = reinicia saltos
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
            if (anim != null) anim.SetTrigger("SALTO");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            saltosRestantes--;
        }

        // ANIMACIONES
        if (anim != null)
        {
            anim.SetFloat("MOVIMIENTO", Mathf.Abs(move));
            anim.SetBool("ENSUELO", enSuelo);
        }
    }

    // Llamar cuando el jugador reciba daño (por colisión en trampas)
    public void RecibirDaño()
    {
        if (isDead) return;

        if (anim != null) anim.SetTrigger("DAÑO");
        GameManager.instance.RestarVida(1);

        // si vidas llegaron a 0, activar estado de "muerte" permanente
        if (GameManager.instance != null && GameManager.instance.vidas <= 0)
        {
            PerderJuego();
        }
    }

    // Método público: cuando se pierde, paraliza todo y bloquea controles del jugador
    public void PerderJuego()
    {
        if (isDead) return;
        isDead = true;

        // Detener movimiento físico inmediato
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false; 
        }

        // Asegurar que el Animator siga reproduciéndose aunque Time.timeScale = 0
        if (anim != null)
        {
            anim.updateMode = AnimatorUpdateMode.UnscaledTime;

            if (!string.IsNullOrEmpty(dañoStateName) && dañoStateHash != 0)
            {
                // reproducir directamente el estado de daño (asegúrate que ese clip esté en loop en el Animator)
                anim.Play(dañoStateHash, -1, 0f);
            }
            else
            {
                // si no hay nombre de estado, disparar trigger DAÑO (y configurar el Animator para quedarse en un estado en loop)
                anim.SetTrigger("DAÑO");
            }
        }

        // Bloquear controles y actualizaciones del componente Player
        enabled = false;

        // Congelar el resto del juego (puedes mantener UI y Animator con updateMode unscaled)
        Time.timeScale = 0f;

        // Mostrar la UI de derrota (GameManager se encarga de la UI)
        if (GameManager.instance != null)
            GameManager.instance.PerderJuego("La princesa ha caído...");
        else
            Debug.LogWarning("GameManager no encontrado al mostrar derrota.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;
        if (other.CompareTag("Trampa"))
            RecibirDaño();
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}