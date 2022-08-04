using UnityEngine;

/// <summary>
/// Clase que se encarga de que la cámara siga constantemente al jugador.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// <summary>
    /// La posición del jugador.
    /// </summary>
    [SerializeField] Transform target = null;
    /// <summary>
    /// La suavidad con la que se mueve la cámara.
    /// </summary>
    [SerializeField] [Range (0.0f , 1.0f)] float smoothing = 0.5f;
    /// <summary>
    /// Verdadero para corregir el movimiento de la cámara. Falso para no hacerlo.
    /// </summary>
    bool interpolation = true;
    /// <summary>
    /// La distancia entre la cámara y el jugador.
    /// </summary>
    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 cameraPosition = target.position + offset;

        if (interpolation)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPosition, smoothing);
        }
        else
        {
            transform.position = cameraPosition;
        }
    }
}