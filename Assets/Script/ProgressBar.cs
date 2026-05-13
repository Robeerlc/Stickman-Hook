using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [Header("Referencias de la Escena")]
    public Transform player;       // Tu Stickman
    public Transform startLine;    // El PuntoInicio
    public Transform finishLine;   // El PuntoMeta

    [Header("Referencia de la UI")]
    public Slider progressBar;     // Tu Barra de Progreso (Slider)

    void Start()
    {
        // Aseguramos que el Slider va de 0 a 1
        if (progressBar != null)
        {
            progressBar.minValue = 0f;
            progressBar.maxValue = 1f;
            progressBar.value = 0f;
        }
    }

    void Update()
    {
        // Si falta algo por asignar, no hacemos nada para evitar errores
        if (player == null || startLine == null || finishLine == null || progressBar == null)
            return;

        // Calculamos la distancia total del nivel (eje X)
        float distanceTotal = finishLine.position.x - startLine.position.x;
        
        // Calculamos cuánto ha avanzado el jugador (eje X)
        float distancePlayer = player.position.x - startLine.position.x;
        
        if (distanceTotal > 0)
        {
            // Calculamos el porcentaje
            float progress = distancePlayer / distanceTotal;
            
            // Llenamos la barra (Mathf.Clamp01 evita que pase del 100% o baje del 0%)
            progressBar.value = Mathf.Clamp01(progress);
        }
    }
}