using UnityEngine;
using UnityEngine.AI;

public class WarrokEnemy : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] waypoints;
    public float waitTime = 2f;

    [Header("Combat")]
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public int attackDamage = 10;
    public float attackCooldown = 1.5f;

    private NavMeshAgent agent;
    private Animator animator;
    private Health health;
    private Transform player;
    private Health playerHealth;

    private int currentWaypoint;
    private float waitTimer;
    private bool waiting;
    private float nextAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();

        if (GetComponent<Collider>() == null)
        {
            CapsuleCollider col = gameObject.AddComponent<CapsuleCollider>();
            col.center = new Vector3(0, 1f, 0);
            col.height = 2f;
            col.radius = 0.5f;
        }

        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            player = go.transform;
            playerHealth = go.GetComponent<Health>();
        }

        if (waypoints == null || waypoints.Length == 0)
        {
            enabled = false;
            return;
        }

        currentWaypoint = 0;
        agent.SetDestination(waypoints[0].position);
        animator.SetBool("IsWalking", true);
    }

    void Update()
    {
        if (health == null) return;

        bool dead = health.CurrentHealth <= 0;
        animator.SetBool("IsDead", dead);
        if (dead)
        {
            agent.isStopped = true;
            return;
        }

        float distToPlayer = player != null
            ? Vector3.Distance(transform.position, player.position)
            : Mathf.Infinity;

        if (distToPlayer <= detectionRange && playerHealth != null)
        {
            if (distToPlayer <= attackRange)
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
                animator.SetBool("IsWalking", false);
                if (Time.time >= nextAttackTime)
                {
                    playerHealth.TakeDamage(attackDamage);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.SetBool("IsWalking", true);
            }
            return;
        }
        else if (agent.isStopped)
        {
            agent.isStopped = false;
            if (waypoints != null && waypoints.Length > 0)
                agent.SetDestination(waypoints[currentWaypoint].position);
        }

        Patroll();
    }

    void Patroll()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        if (waiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                waiting = false;
                NextWaypoint();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waiting = true;
            waitTimer = 0f;
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    void NextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        agent.SetDestination(waypoints[currentWaypoint].position);
        agent.isStopped = false;
        animator.SetBool("IsWalking", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
