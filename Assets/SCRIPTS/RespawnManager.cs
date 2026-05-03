using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    [Tooltip("Posicion donde reaparecera el jugador")]
    public Transform spawnPoint;

    [Tooltip("Altura minima antes de considerar caida")]
    public float fallThreshold = -10f;

    [Tooltip("Tiempo que tarda en reaparecer")]
    public float respawnDelay = 1f;

    private Transform player;
    private bool isRespawning = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (player == null)
        {
            Debug.LogError("RespawnManager: No se encontro el jugador con tag 'Player'");
            this.enabled = false;
        }
        if (spawnPoint == null)
        {
            Debug.LogWarning("RespawnManager: No hay spawnPoint asignado. Usando posicion inicial del jugador.");
        }
    }

    void Update()
    {
        if (player == null || isRespawning) return;

        if (player.position.y < fallThreshold)
        {
            StartCoroutine(Respawn());
        }
    }

    System.Collections.IEnumerator Respawn()
    {
        isRespawning = true;

        Ataque ataque = player.GetComponent<Ataque>();
        if (ataque != null)
        {
            ataque.enabled = false;
        }

        yield return new WaitForSeconds(respawnDelay);

        if (spawnPoint != null)
        {
            player.position = spawnPoint.position;
            player.rotation = spawnPoint.rotation;
        }
        else
        {
            player.position = Vector3.zero;
        }

        if (ataque != null)
        {
            ataque.enabled = true;
        }

        isRespawning = false;
    }
}
