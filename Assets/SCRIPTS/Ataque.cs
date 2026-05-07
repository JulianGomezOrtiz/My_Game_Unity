using UnityEngine;
using UnityEngine.InputSystem;

public class Ataque : MonoBehaviour
{
    private Animator _animator;
    private StarterAssets.ThirdPersonController _fps;
    private bool isAttacking = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _fps = GetComponent<StarterAssets.ThirdPersonController>();
    }

    void Update()
    {
    }

    void atacar()
    {
        _animator.SetTrigger("ataque");
        Invoke("ReenableController", 1f);
    }

    void ReenableController()
    {
        _fps.enabled = true;
        isAttacking = false;
    }
}