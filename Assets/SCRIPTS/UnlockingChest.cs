using UnityEngine;

public class Unlocking : MonoBehaviour
{
    public GameObject Cabierto;
    public GameObject Ccerrado;
    public GameObject moneda;

    [Tooltip("Puntos extra al abrir el cofre")]
    public int puntosBonus = 5;

    [Tooltip("Llaves consumidas al abrir")]
    public bool consumirLlaves = true;

    private bool abierto = false;

    void OnTriggerEnter(Collider other)
    {
        PirateBehaviour pirate = other.GetComponent<PirateBehaviour>();

        if (pirate != null && pirate.getPuntos() >= 3 && !abierto)
        {
            Ccerrado.SetActive(false);
            Cabierto.SetActive(true);
            moneda.SetActive(true);
            abierto = true;

            pirate.SetPuntos(pirate.getPuntos() + puntosBonus);
            Debug.Log("Cofre abierto! +" + puntosBonus + " puntos");
        }
    }
}