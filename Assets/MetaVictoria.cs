using UnityEngine;

public class MetaVictoria : MonoBehaviour
{
    public GameObject textoVictoria; 
    public GameObject camaraPrincipal; // La cámara que sigues al jugar
    public GameObject camaraVictoria;  // La cámara ampliada de la película

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Mostrar las letras de Misión Cumplida
            if (textoVictoria != null) textoVictoria.SetActive(true);
            
            // 2. Apagar la cámara del jugador y encender la cámara ampliada
            if (camaraPrincipal != null) camaraPrincipal.SetActive(false);
            if (camaraVictoria != null) camaraVictoria.SetActive(true);

            // 3. Detener el juego por completo
            Time.timeScale = 0f; 
        }
    }
}