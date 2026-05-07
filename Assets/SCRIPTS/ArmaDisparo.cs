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
        bullet.GetComponent<Bullet>().damage = damage;
    }
}
