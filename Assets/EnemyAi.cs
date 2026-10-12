using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; 

public class EnemyAI : MonoBehaviour
{
    [Header("Configuración de Persecución y Atrape")]
    public Transform jugador; 
    public float distanciaPersecucion = 6.0f; // Distancia a la que te empieza a seguir
    public float distanciaAtrape = 2.0f;      // Distancia a la que te atrapa
    
    [Header("Configuración de Patrulla")]
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    private NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        // Iniciar patrulla hacia el primer punto si existen waypoints configurados
        if (waypoints != null && waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    void Update()
    {
        if (jugador != null)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);
            
            // 1. Condición de derrota (Atrapó al jugador)
            if (distancia <= distanciaAtrape)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return; // Evita que el resto del código se ejecute tras reiniciar
            }

            // 2. Comportamiento A: Persecución
            if (distancia <= distanciaPersecucion)
            {
                agent.SetDestination(jugador.position);
            }
            // 3. Comportamiento B: Patrullaje
            else
            {
                Patrullar();
            }

            // 4. Animar según la velocidad real del NavMeshAgent
            anim.SetFloat("Speed", agent.velocity.magnitude);
        }
    }

    void Patrullar()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        // Si el enemigo llega al waypoint actual, avanza al siguiente en la lista
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
}