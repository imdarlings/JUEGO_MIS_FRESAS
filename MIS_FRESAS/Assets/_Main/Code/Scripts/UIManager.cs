using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Referencias de texto (HUD)")]
    public TMP_Text textoVidas;
    public TMP_Text textoFresas;
    public TMP_Text textoTiempo;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
       
        if (GameManager.instance == null) return;

        ActualizarHUD();
    }

    void ActualizarHUD()
    {
        if (textoVidas != null)
            textoVidas.text = "Vidas" + GameManager.instance.vidas.ToString();

        if (textoFresas != null)
            textoFresas.text = "Fresas" + GameManager.instance.fresasRecolectadas.ToString();

        if (textoTiempo != null)
            textoTiempo.text = "tiempo" + Mathf.Ceil(GameManager.instance.tiempoRestante).ToString() + "s";
    }
}
