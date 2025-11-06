using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public string nombreEscenaJuego = "NIVEL"; 

    public void Jugar()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(nombreEscenaJuego);
    }

    public void Salir()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }

    public GameObject panelInstrucciones;

    public void MostrarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(true);
    }

    public void CerrarInstrucciones()
    {
        if (panelInstrucciones != null)
            panelInstrucciones.SetActive(false);
    }
}

