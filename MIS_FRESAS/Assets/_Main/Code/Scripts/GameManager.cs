using System.Collections;
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
    private bool juegoTerminado = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Debug.Log("GameManager iniciado");

        // Intentar reconectar UIManager de varias formas
        if (uiManager == null)
        {
            uiManager = UIManager.instance;
            if (uiManager == null)
            {
                uiManager = Object.FindFirstObjectByType<UIManager>();
            }

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
        if (!juegoActivo || juegoTerminado) return;

        // Contador del tiempo
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            tiempoRestante = 0;
            PerderJuego("¡Se acabó el tiempo!");
        }

        // Actualiza la UI del tiempo cada frame (puedes optimizar para no llamar cada frame)
        uiManager?.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
    }

    //  Fresas
    public void SumarFresa()
    {
        if (juegoTerminado) return;

        fresasRecolectadas++;
        uiManager?.ActualizarFresas(fresasRecolectadas);
    }

    //  Tiempo extra
    public void SumarTiempo(float segundosExtra = 10f)
    {
        if (juegoTerminado) return;

        tiempoRestante += segundosExtra;
        uiManager?.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
    }

    // Pociones
    public void SumarVida(int cantidad = 1)
    {
        if (juegoTerminado) return;

        vidas = Mathf.Min(vidas + cantidad, 5);
        uiManager?.ActualizarVidas(vidas);
    }

    // Arañas / daño
    public void RestarVida(int cantidad = 1)
    {
        if (juegoTerminado) return;

        vidas -= cantidad;
        uiManager?.ActualizarVidas(vidas);

        if (vidas <= 0)
        {
            vidas = 0;
            PerderJuego("¡Te quedaste sin vidas!");
        }
    }

    // Pergamino
    public void LeerPergamino()
    {
        if (juegoTerminado) return;

        leyoPergamino = true;
        Debug.Log("solo necesitas 5 fresas.");
        uiManager?.ActualizarPergamino();
    }

    // Príncipe
    public void LlegarAlPrincipe()
    {
        if (!juegoActivo || juegoTerminado) return;

        int meta = leyoPergamino ? 5 : 10;
        if (fresasRecolectadas >= meta)
        {
            GanarJuego();
        }
        else
        {
            Debug.Log("Aún no tienes suficientes fresas");
        }
    }

    public void GanarJuego()
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        juegoActivo = false;
        Time.timeScale = 0f;

        Debug.Log("¡GANASTE!");
        uiManager?.MostrarVictoria();
    }

    public void PerderJuego(string razon = "¡PERDISTE!")
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        juegoActivo = false;
        Time.timeScale = 0f;

        Debug.Log(razon);
        uiManager?.MostrarDerrota(razon);

        StartCoroutine(ReiniciarDespues(5f));
    }

    private IEnumerator ReiniciarDespues(float segundos)
    {
        yield return new WaitForSecondsRealtime(segundos);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        juegoTerminado = false;
    }
}
