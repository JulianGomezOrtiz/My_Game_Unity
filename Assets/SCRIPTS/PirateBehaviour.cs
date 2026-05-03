using UnityEngine;
using System.Collections;

public class PirateBehaviour : MonoBehaviour
{
    private int puntos;

    void Start()
    {
        puntos = 0;
    }

    public int getPuntos()
    {
        return puntos;
    }

    public void SetPuntos(int valor)
    {
        puntos = valor;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Llave")) {
            puntos += 1;
            Destroy(other.gameObject, 0.09f);
            if (puntos >= 3) {
                Debug.Log("¡Tenés las 3 llaves! Abrí el cofre.");
            }
        }
    }
}
