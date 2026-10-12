using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int nucleosRecolectados = 0;
    private int totalNucleos = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nucleo"))
        {
            nucleosRecolectados++;
            Destroy(other.gameObject);
            Debug.Log("Núcleos: " + nucleosRecolectados + " / " + totalNucleos);

            if (nucleosRecolectados >= totalNucleos)
            {
                Debug.Log("¡Compuerta de escape abierta!");
            }
        }
    }
}