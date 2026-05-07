using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage;
    public float lifetime = 2f;

    void Start()
    {
        GetComponent<Rigidbody>().linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health != null)
            health.TakeDamage(damage);
        Destroy(gameObject);
    }
}
