using UnityEngine;

public class Unlocking : MonoBehaviour
{
    public GameObject Cabierto;
    public GameObject Ccerrado;
    public GameObject moneda;

    private bool abierto = false;

    void OnTriggerEnter(Collider other)
    {
        PirateBehaviour pirate = other.GetComponent<PirateBehaviour>();

        if (pirate != null && pirate.getPuntos() == 3 && !abierto)
        {
            Ccerrado.SetActive(false);
            Cabierto.SetActive(true);

            moneda.SetActive(true);

            abierto = true;

            Debug.Log("Cofre abierto y moneda mostrada");
        }
    }
}