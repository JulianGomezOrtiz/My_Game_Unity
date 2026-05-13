using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage;
    public float lifetime = 2f;
    public float ignoreCollisionTime = 0f;

    private float spawnTime;

    void Awake()
    {
        spawnTime = Time.time;
    }

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (ignoreCollisionTime > 0f && Time.time - spawnTime < ignoreCollisionTime)
            return;

        var health = other.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage);
        Destroy(gameObject);
    }
}
