using UnityEngine;
using UnityEngine.InputSystem;

public class Ataque : MonoBehaviour
{
    private Animator _animator;
    private StarterAssets.ThirdPersonController _fps;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _fps = GetComponent<StarterAssets.ThirdPersonController>();
    }

    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            _fps.enabled = false;
            atacar();
        }
    }

    void atacar()
    {
        _animator.SetTrigger("ataque");
    }
}