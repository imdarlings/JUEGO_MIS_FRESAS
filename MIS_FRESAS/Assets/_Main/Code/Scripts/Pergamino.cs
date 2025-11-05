using UnityEngine;

public class Pergamino : MonoBehaviour
{
    private bool pergaminoLeido = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !pergaminoLeido)
        {
            UIManager.instance.MostrarPanelPergaminoDecision(true, this);
        }
    }

    public void LeerPergamino()
    {
        pergaminoLeido = true;
        GameManager.instance.LeerPergamino();
        UIManager.instance.MostrarPanelPergaminoDecision(false, this);
        UIManager.instance.MostrarPanelPergaminoLeido(true);
        gameObject.SetActive(false); // Desaparece el pergamino
    }

    public void IgnorarPergamino()
    {
        UIManager.instance.MostrarPanelPergaminoDecision(false, this);
    }
}