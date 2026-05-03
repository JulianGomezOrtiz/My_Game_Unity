using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class BanditDialog : MonoBehaviour
{
    [Tooltip("Panel de diálogo del Bandit (GameObject del Canvas)")]
    public GameObject dialogPanel;

    [Tooltip("Componente TextMeshPro donde se escribe el mensaje")]
    public TextMeshProUGUI textoUI;

    [TextArea(3, 10)]
    public string mensaje = "Texto del diálogo aquí...";

    [Tooltip("Segundos entre cada letra")]
    public float velocidadLetra = 0.05f;

    private bool jugadorCerca = false;
    private Coroutine corutinaEscribiendo;
    private bool estaEscribiendo = false;

    void Update()
    {
        if (jugadorCerca && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            if (dialogPanel != null && dialogPanel.activeSelf)
            {
                if (estaEscribiendo)
                {
                    StopCoroutine(corutinaEscribiendo);
                    textoUI.text = mensaje;
                    estaEscribiendo = false;
                }
                else
                {
                    dialogPanel.SetActive(false);
                }
            }
            else if (dialogPanel != null)
            {
                dialogPanel.SetActive(true);
                textoUI.text = "";
                corutinaEscribiendo = StartCoroutine(EfectoTypewriter(mensaje));
            }
        }
    }

    IEnumerator EfectoTypewriter(string textoCompleto)
    {
        estaEscribiendo = true;

        for (int i = 0; i <= textoCompleto.Length; i++)
        {
            textoUI.text = textoCompleto.Substring(0, i);
            yield return new WaitForSeconds(velocidadLetra);
        }

        estaEscribiendo = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (dialogPanel != null && dialogPanel.activeSelf)
            {
                if (corutinaEscribiendo != null)
                    StopCoroutine(corutinaEscribiendo);
                dialogPanel.SetActive(false);
            }
        }
    }
}
