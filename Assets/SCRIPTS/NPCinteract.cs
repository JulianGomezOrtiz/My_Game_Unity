using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInteract : MonoBehaviour
{
    [TextArea(3, 10)]
    public string dialogText = "Hola, soy un NPC. ¡Bienvenido!";
    public float typingSpeed = 0.05f;

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Interactuar();
        }
    }

    void Interactuar()
    {
        if (DialogManager.Instance != null)
        {
            DialogManager.Instance.ShowDialog(dialogText, typingSpeed);
        }
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
        }
    }
}