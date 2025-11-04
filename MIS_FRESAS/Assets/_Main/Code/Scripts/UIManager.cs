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
    public TMP_Text textoDerrotaMensaje;
    public GameObject panelPausa;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        // No hacemos nada aquí por defecto
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

    // Mostrar Pantalla de Derrota con mensaje
    public void MostrarDerrota(string mensaje)
    {
        panelVictoria?.SetActive(false);
        panelPausa?.SetActive(false);
        panelDerrota?.SetActive(true);
        if (textoDerrotaMensaje != null)
            textoDerrotaMensaje.text = mensaje;
    }

    // Mostrar/Ocultar pausa
    public void MostrarPausa(bool enPausa)
    {
        panelPausa?.SetActive(enPausa);
    }
}
