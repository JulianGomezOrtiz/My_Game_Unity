using UnityEngine;

public class LedgeDetector : MonoBehaviour
{
    [Tooltip("Distancia horizontal para detectar borde")]
    public float detectDistance = 0.5f;

    [Tooltip("Capas consideradas suelo")]
    public LayerMask groundLayers;

    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        if (agent == null || !agent.enabled) return;

        Vector3 forward = transform.forward * detectDistance;
        RaycastHit hit;

        if (!Physics.Raycast(transform.position + Vector3.up * 0.5f, forward, out hit, detectDistance, groundLayers))
        {
            agent.isStopped = true;
            Invoke("PickNewDestination", 1f);
        }
    }

    void PickNewDestination()
    {
        agent.isStopped = false;
    }
}
