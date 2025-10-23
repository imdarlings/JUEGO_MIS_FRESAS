using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Jugador")]
    public int vidas = 3;
    public int fresasRecolectadas = 0;
    public bool leyoPergamino = false;

    [Header("Tiempo")]
    public float tiempoRestante = 60f;
    private bool juegoActivo = true;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (!juegoActivo) return;

        // Contador del tiempo
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            Perder("¡Se acabó el tiempo!");
        }
    }

    //  Fresas
    public void SumarFresa()
    {
        fresasRecolectadas++;
    }

    //  Estrellas
    public void SumarTiempo(float segundosExtra = 10f)
    {
        tiempoRestante += segundosExtra;
    }

    // Pociones
    public void SumarVida(int cantidad = 1)
    {
        vidas = Mathf.Min(vidas + cantidad, 5);
    }

    // Arañas
    public void RestarVida(int cantidad = 1)
    {
        vidas -= cantidad;
        if (vidas <= 0)
        {
            Perder("¡Te quedaste sin vidas!");
        }
    }

    // Pergamino
    public void LeerPergamino()
    {
        leyoPergamino = true;
        Debug.Log("olo necesitas 5 fresas.");
    }

    // Príncipe
    public void LlegarAlPrincipe()
    {
        if (!juegoActivo) return;

        int meta = leyoPergamino ? 5 : 10;
        if (fresasRecolectadas >= meta)
        {
            Ganar();
        }
        else
        {
            Debug.Log("Aún no tienes suficientes fresas");
        }
    }

    //Ganar
    void Ganar()
    {
        juegoActivo = false;
        Debug.Log("¡Ganaste!");
        // SceneManager.LoadScene("Victoria");
    }

    // Perder
    void Perder(string razon)
    {
        juegoActivo = false;
        Debug.Log("Perdiste" + razon);
        // SceneManager.LoadScene("Derrota");
    }
}
