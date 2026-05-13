using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("Stats")]
    public float fireRate = 0.6f;
    public int damage = 15;
    public float range = 40f;
    public float leadFactor = 0.5f;
    public float bulletScale = 0.5f;

    private Transform player;
    private CharacterController playerController;
    private float nextFireTime;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null)
        {
            player = go.transform;
            playerController = go.GetComponent<CharacterController>();
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > range) return;

        Vector3 targetPos = player.position;
        if (playerController != null && playerController.velocity.magnitude > 0.1f)
        {
            targetPos += playerController.velocity * leadFactor;
        }

        transform.LookAt(targetPos);

        if (Time.time >= nextFireTime)
        {
            Fire(targetPos);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Fire(Vector3 targetPos)
    {
        Transform origin = firePoint != null ? firePoint : transform;
        Vector3 dir = (targetPos - origin.position).normalized;
        Vector3 spawnPos = origin.position + dir * 1.5f;

        GameObject bullet;
        if (bulletPrefab != null)
        {
            bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(dir));
            bullet.transform.localScale = Vector3.one * bulletScale;
        }
        else
        {
            bullet = CreateBullet(spawnPos, dir);
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.useGravity = false;

        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.damage = damage;
            b.ignoreCollisionTime = 0.15f;
        }

        Debug.Log("Cannon disparó");
    }

    GameObject CreateBullet(Vector3 position, Vector3 direction)
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
        obj.transform.localScale = Vector3.one * bulletScale;
        obj.name = "CannonBall";

        obj.GetComponent<SphereCollider>().isTrigger = true;

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) rb = obj.AddComponent<Rigidbody>();
        rb.useGravity = false;
        rb.mass = 0.5f;

        obj.AddComponent<Bullet>();
        return obj;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
