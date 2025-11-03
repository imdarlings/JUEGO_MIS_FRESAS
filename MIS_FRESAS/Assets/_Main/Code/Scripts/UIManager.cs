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
           
        for (int i = 0; i < barraVida.Length; i++)
        {
            barraVida[i].enabled = (i < vidas);
        }
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
        if (GameManager.instance == null) return;
        if (textoPergaminpo == null) return;

        textoPergaminpo.text = GameManager.instance.leyoPergamino ? "Sí" : "No";
    }
}
