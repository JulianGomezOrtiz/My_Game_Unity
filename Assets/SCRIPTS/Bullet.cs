using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 50f;
    public int damage;
    public float lifetime = 2f;
    public float ignoreCollisionTime = 0f;
    [HideInInspector] public GameObject owner;

    private float spawnTime;
    private bool wasHit = false;

    private Collider _collider;

    void Awake()
    {
        spawnTime = Time.time;

        _collider = GetComponent<Collider>();
        if (_collider == null)
        {
            _collider = gameObject.AddComponent<SphereCollider>();
            Debug.Log("Bala: Collider creado automáticamente");
        }
        _collider.isTrigger = true;

        if (GetComponent<Rigidbody>() == null)
        {
            Rigidbody rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            Debug.Log("Bala: Rigidbody creado automáticamente");
        }
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = transform.forward * speed;
            Debug.Log("Bala spawn: pos=" + transform.position + " dir=" + transform.forward + " vel=" + rb.linearVelocity + " speed=" + speed);
        }
        else
        {
            Debug.LogError("Bala: No tiene Rigidbody");
        }
        Invoke(nameof(DestroyByTimeout), lifetime);
    }

    void DestroyByTimeout()
    {
        if (!wasHit)
            Debug.Log("Bala expiró sin impactar nada. Posición final: " + transform.position);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    void OnCollisionEnter(Collision collision)
    {
        Hit(collision.collider);
    }

    void Hit(Collider other)
    {
        if (ignoreCollisionTime > 0f && Time.time - spawnTime < ignoreCollisionTime)
            return;

        if (owner != null && other.transform.root == owner.transform.root)
            return;

        Debug.Log("Bala impactó: " + other.name + " (tag: " + other.tag + ")");

        var health = other.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
            Debug.Log("Daño aplicado: " + damage + " a " + other.name);
        }
        else
        {
            Debug.Log("Bala: " + other.name + " NO tiene Health");
        }

        wasHit = true;
        CancelInvoke(nameof(DestroyByTimeout));
        Destroy(gameObject);
    }
}
