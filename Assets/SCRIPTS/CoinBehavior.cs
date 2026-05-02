using UnityEngine;

public class Moneda : MonoBehaviour
{
    public float velocidadRotacion = 100f;

    private bool puedeRecogerse = false;

    void Start()
    {
        // Espera un momento antes de permitir recoger la moneda
        Invoke("ActivarRecogida", 0.3f);
    }

    void Update()
    {
        // Rotación de la moneda 
        transform.Rotate(0, 0, -velocidadRotacion * Time.deltaTime);
    }

    void ActivarRecogida()
    {
        puedeRecogerse = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!puedeRecogerse) return;

        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}