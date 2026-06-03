using UnityEngine;
using UnityEngine.InputSystem;

public class ArmaDisparo : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public int damage = 10;

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Click izquierdo detectado");
            Disparar();
        }
    }

    void Disparar()
    {
        if (bulletPrefab == null || bulletSpawnPoint == null)
        {
            Debug.LogError("Falta bulletPrefab o bulletSpawnPoint");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        Bullet b = bullet.GetComponent<Bullet>();
        if (b != null)
        {
            b.damage = damage;
            b.speed = 50f;
            b.owner = gameObject;
        }

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.useGravity = false;
    }
}
