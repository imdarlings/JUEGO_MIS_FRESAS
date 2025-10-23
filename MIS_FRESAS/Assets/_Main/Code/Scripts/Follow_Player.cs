using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Follow_Player : MonoBehaviour

{
    public Transform objetivo; // arrastra aquí a tu jugador
    public float suavizado = 0.2f;
    private Vector3 velocidad = Vector3.zero;

    [Header("Límites de cámara")]
    public float minX = -14f, maxX = 47f;
    public float minY = -7f, maxY = 37f;

    void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 posicionDeseada = new Vector3(objetivo.position.x, objetivo.position.y, transform.position.z);

        // Limitar cámara dentro del mapa
        posicionDeseada.x = Mathf.Clamp(posicionDeseada.x, minX, maxX);
        posicionDeseada.y = Mathf.Clamp(posicionDeseada.y, minY, maxY);

        // Movimiento suave (lerp)
        transform.position = Vector3.SmoothDamp(transform.position, posicionDeseada, ref velocidad, suavizado);
    }
}