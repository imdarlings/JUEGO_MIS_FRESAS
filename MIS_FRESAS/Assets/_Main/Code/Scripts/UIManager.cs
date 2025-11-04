using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("CONTADOR")]
    public Image[] barraVida;
    public TMP_Text textoFresas;
    public TMP_Text textoTiempo;
    public TMP_Text textoPergaminpo;

    [Header("PANELES")]
    public GameObject panelVictoria;
    public GameObject panelDerrota;
    public Button botonReiniciar;
    public TMP_Text textoBotonReiniciar;
    public GameObject panelPausa;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.instance == null) return;
    }

    public void ActualizarVidas(int vidas)
    {
        if (barraVida == null || barraVida.Length == 0) return;

       
        vidas = Mathf.Clamp(vidas, 0, barraVida.Length - 1);

    
        for (int i = 0; i < barraVida.Length; i++)
        {
            barraVida[i].enabled = false;
        }

        
        barraVida[vidas].enabled = true;
    }

    public void ActualizarFresas(int cantidad)
    {
        if (textoFresas == null) return;
        textoFresas.text = cantidad.ToString();
    }

    public void ActualizarTiempo(int segundos)
    {
        if (textoTiempo == null) return;
        textoTiempo.text = Mathf.Max(0, segundos).ToString();
    }

    public void ActualizarPergamino()
    {
        if (textoPergaminpo == null || GameManager.instance == null) return;
        textoPergaminpo.text = GameManager.instance.leyoPergamino ? "Sí" : "No";
    }

    // Mostrar Pantalla de Victoria
    public void MostrarVictoria()
    {
        panelDerrota?.SetActive(false);
        panelPausa?.SetActive(false);
        panelVictoria?.SetActive(true);
    }


    public void MostrarDerrota(string mensaje)
    {
        if (panelDerrota != null)
            panelDerrota.SetActive(true);

        if (botonReiniciar != null)
        {
            botonReiniciar.interactable = true;
            botonReiniciar.onClick.RemoveAllListeners();
            botonReiniciar.onClick.AddListener(() =>
            {
                GameManager.instance.ReiniciarEscena();
            });
        }
    }

    public void ActualizarTextoBotonReiniciar(float tiempo)
    {
        if (textoBotonReiniciar != null)
            textoBotonReiniciar.text = $"Reiniciar ({Mathf.CeilToInt(tiempo)}s)";
    }

    // Mostrar/Ocultar pausa
    public void MostrarPausa(bool enPausa)
    {
        panelPausa?.SetActive(enPausa);
    }
}
