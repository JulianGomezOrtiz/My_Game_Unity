using UnityEngine;
using UnityEngine.AI;

public class NavegationPatrol : MonoBehaviour
{
    NavMeshAgent Agente;
    int i;
    public GameObject[] puntos;

    void Start()
    {
        Agente = GetComponent<NavMeshAgent>();
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