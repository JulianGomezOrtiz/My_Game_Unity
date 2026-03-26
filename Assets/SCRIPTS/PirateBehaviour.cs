using UnityEngine;
using System.Collections;
using UnityEditor;

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

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Llave") {
            puntos += 1;
            Destroy(other.gameObject, 0.09f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("A collider is inside the DoorObject trigger");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("A collider has exited the DoorObject trigger");
    }
}
