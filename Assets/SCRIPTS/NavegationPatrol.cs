using UnityEngine;
using UnityEngine.AI;

public class NavegationPatrol : MonoBehaviour
{
    Vector3 posicionInicial;
    NavMeshAgent Agente;
    int i;
    public GameObject[] puntos;

    void Awake()
    {
        posicionInicial = transform.position;
    }

    void Start()
    {
        if (puntos == null || puntos.Length == 0)
        {
            Debug.LogWarning("NavegationPatrol: No hay waypoints asignados.");
            this.enabled = false;
            return;
        }
        Agente = GetComponent<NavMeshAgent>();
        Agente.Warp(posicionInicial);
        i = 0;
        Agente.SetDestination(puntos[i].transform.position);
    }

    void Update()
    {
        if (!Agente.pathPending && Agente.remainingDistance < 0.5f)
        {
            i++;

            if (i >= puntos.Length)
                i = 0;

            Agente.SetDestination(puntos[i].transform.position);
        }
    }
}