using UnityEngine;
using UnityEngine.AI;

public class NavegationPatrol : MonoBehaviour
{
    Vector3 posicionInicial;
    NavMeshAgent Agente;
    Animator animator;
    Health health;
    int i;
    public GameObject[] puntos;

    void Awake()
    {
        posicionInicial = transform.position;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        if (health != null)
            health.OnDied += OnDeath;

        if (puntos == null || puntos.Length == 0)
        {
            Debug.LogWarning("NavegationPatrol: No hay waypoints asignados.");
            this.enabled = false;
            return;
        }
        Agente = GetComponent<NavMeshAgent>();
        if (Agente == null)
        {
            Debug.LogError("NavegationPatrol: No hay NavMeshAgent.");
            this.enabled = false;
            return;
        }
        Agente.Warp(posicionInicial);
        i = 0;
        Agente.SetDestination(puntos[i].transform.position);
        animator?.SetBool("IsWalking", true);
    }

    void Update()
    {
        if (Agente == null || !Agente.isOnNavMesh) return;
        if (health != null && health.CurrentHealth <= 0) return;

        if (!Agente.pathPending && Agente.remainingDistance < 0.5f)
        {
            i = (i + 1) % puntos.Length;
            Agente.SetDestination(puntos[i].transform.position);
            animator?.SetBool("IsWalking", true);
        }
        else if (!Agente.pathPending)
        {
            animator?.SetBool("IsWalking", Agente.remainingDistance > Agente.stoppingDistance);
        }
    }

    void OnDeath()
    {
        animator?.SetBool("IsDead", true);
        Agente.isStopped = true;
        this.enabled = false;
    }

    void OnDestroy()
    {
        if (health != null)
            health.OnDied -= OnDeath;
    }
}