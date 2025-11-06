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
    private bool juegoPausado = false;

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
            uiManager = UIManager.instance;
            if (uiManager == null)
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
        if (!juegoActivo || juegoTerminado) return;

      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                EstadoDelJuego("Play");
            else
                EstadoDelJuego("Pausa");
        }

       
        if (!juegoPausado)
        {
            tiempoRestante -= Time.deltaTime;
            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                PerderJuego("¡Se acabó el tiempo!");
            }

            uiManager?.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
        }
    }


    // Lógica del juego

    public void SumarFresa()
    {
        if (juegoTerminado) return;
        fresasRecolectadas++;
        uiManager?.ActualizarFresas(fresasRecolectadas);
    }

    public void SumarTiempo(float segundosExtra = 10f)
    {
        if (juegoTerminado) return;
        tiempoRestante += segundosExtra;
        uiManager?.ActualizarTiempo(Mathf.CeilToInt(tiempoRestante));
    }

    public void SumarVida(int cantidad = 1)
    {
        if (juegoTerminado) return;
        vidas = Mathf.Min(vidas + cantidad, 5);
        uiManager?.ActualizarVidas(vidas);
    }

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

    public void LeerPergamino()
    {
        if (juegoTerminado) return;
        leyoPergamino = true;
        Debug.Log("Solo necesitas 5 fresas.");
        uiManager?.ActualizarPergamino();
    }

    public void LlegarAlPrincipe()
    {
        if (!juegoActivo || juegoTerminado) return;
        int meta = leyoPergamino ? 5 : 10;

        if (fresasRecolectadas >= meta)
            GanarJuego();
        else
            Debug.Log("Aún no tienes suficientes fresas");
    }

    
    // Estados del juego

    public void GanarJuego()
    {
        if (juegoTerminado) return;
        juegoTerminado = true;
        juegoActivo = false;
        Time.timeScale = 0f;

        Debug.Log("¡GANASTE!");
        uiManager?.MostrarVictoria();
    }

    public void PerderJuego(string mensaje)
    {
        if (juegoTerminado) return;

        juegoTerminado = true;
        juegoActivo = false;
        Debug.Log(mensaje);

        uiManager?.MostrarDerrota(mensaje);

        var player = Object.FindFirstObjectByType<Player>();
        if (player != null)
            player.PerderJuego();
        else
            Time.timeScale = 0f;

        StartCoroutine(ReiniciarAutomatico(10f));
    }

    private IEnumerator ReiniciarAutomatico(float segundos)
    {
        float tiempoRestante = segundos;

        while (tiempoRestante > 0)
        {
            uiManager?.ActualizarTextoBotonReiniciar(tiempoRestante);
            yield return new WaitForSecondsRealtime(1f);
            tiempoRestante -= 1f;
        }

        ReiniciarEscena();
    }

    public void ReiniciarEscena()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void EstadoDelJuego(string estado)
    {
        switch (estado)
        {
            case "Ganar":
                GanarJuego();
                break;

            case "Perder":
                PerderJuego("Perdiste");
                break;

            case "Play":
                Time.timeScale = 1f;
                juegoPausado = false;
                uiManager?.MostrarPausa(false);
                break;

            case "Pausa":
                Time.timeScale = 0f;
                juegoPausado = true;
                uiManager?.MostrarPausa(true);
                break;

            case "Reiniciar":
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case "Menu":
                Time.timeScale = 1f;
                SceneManager.LoadScene("INICIO");
                break;

            default:
                Debug.LogWarning("Estado desconocido: " + estado);
                break;
        }
    }
}

