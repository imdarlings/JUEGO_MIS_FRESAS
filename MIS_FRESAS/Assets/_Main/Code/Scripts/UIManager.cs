using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("CONTADOR")]
    public Image[] barraVida;
    public TMP_Text textoFresas;
    public TMP_Text textoTiempo;
    public TMP_Text textoPergamino;

    [Header("PANELES")]
    public GameObject panelVictoria;
    public GameObject panelDerrota;
    public Button botonReiniciar;
    public TMP_Text textoBotonReiniciar;
    public GameObject panelPausa;

    [Header("Paneles de Pergamino")]
    public GameObject panelPergaminoDecision;  // Panel con botones “Leer / Ignorar”
    public GameObject panelPergaminoLeido;     // Panel de mensaje “solo necesitas 5 fresas”
    public Button botonLeer;
    public Button botonIgnorar;
    public Button botonEntendido;


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

   
public void ActualizarPergamino(bool leido)
    {
        if (textoPergamino != null)
            textoPergamino.text = leido ? "SI" : "NO";
    }


    public void MostrarPanelPergaminoDecision(bool mostrar, Pergamino pergamino)
    {
        if (panelPergaminoDecision != null)
            panelPergaminoDecision.SetActive(mostrar);


        if (mostrar && pergamino != null)
        {
            botonLeer.onClick.RemoveAllListeners();
            botonIgnorar.onClick.RemoveAllListeners();

            botonLeer.onClick.AddListener(() => pergamino.LeerPergamino());
            botonIgnorar.onClick.AddListener(() => pergamino.IgnorarPergamino());
        }
    }

    //Panel de mensaje cuando se lee el pergamino
    public void MostrarPanelPergaminoLeido(bool mostrar)
    {
        if (panelPergaminoLeido != null)
            panelPergaminoLeido.SetActive(mostrar);

        if (mostrar)
        {
            botonEntendido.onClick.RemoveAllListeners();
            botonEntendido.onClick.AddListener(() =>
            {
                panelPergaminoLeido.SetActive(false);
            });
        }
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

    internal void ActualizarPergamino()
    {
       if (GameManager.instance == null) return;
         ActualizarPergamino(GameManager.instance.leyoPergamino);
    }
}
