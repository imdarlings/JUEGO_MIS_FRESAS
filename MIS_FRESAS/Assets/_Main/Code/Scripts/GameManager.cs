using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static UIManager uiManager;

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

    private void Start()
    {
        Debug.Log("GameManager iniciado");

        if (uiManager == null)
        {
            uiManager = Object.FindFirstObjectByType<UIManager>();
            if (uiManager != null)
                Debug.Log("UIManager reconectado automáticamente.");
            else
                Debug.LogError("UIManager no encontrado en la escena.");
        }

        ActualizarUIInicial();
    }

    private void ActualizarUIInicial()
    {
        if (uiManager == null) return;

        uiManager.ActualizarFresas(fresasRecolectadas);
        uiManager.ActualizarVidas(vidas);
        uiManager.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
        uiManager.ActualizarPergamino();
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

        // Actualiza la UI del tiempo cada frame (puedes optimizar para no llamar cada frame)
        if (uiManager != null)
            uiManager.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
    }

    //  Fresas
    public void SumarFresa()
    {
        fresasRecolectadas++;
        if (uiManager != null)
            uiManager.ActualizarFresas(fresasRecolectadas);
    }

    //  Estrellas
    public void SumarTiempo(float segundosExtra = 10f)
    {
        tiempoRestante += segundosExtra;
        if (uiManager != null)
            uiManager.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
    }

    // Pociones
    public void SumarVida(int cantidad = 1)
    {
        vidas = Mathf.Min(vidas + cantidad, 5);
        if (uiManager != null)
            uiManager.ActualizarVidas(vidas);
    }

    // Arañas
    public void RestarVida(int cantidad = 1)
    {
        vidas -= cantidad;
        if (uiManager != null)
            uiManager.ActualizarVidas(vidas);

        if (vidas <= 0)
        {
            Perder("¡Te quedaste sin vidas!");
        }
    }

    // Pergamino
    public void LeerPergamino()
    {
        leyoPergamino = true;
        Debug.Log("solo necesitas 5 fresas.");
        if (uiManager != null)
            uiManager.ActualizarPergamino();
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
        Debug.Log("Perdiste: " + razon);
        // SceneManager.LoadScene("Derrota");
    }
}
