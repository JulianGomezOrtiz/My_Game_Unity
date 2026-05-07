using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (!isPaused)
            {
                AbrirMenu();
            }
            else
            {
                CerrarMenu();
            }
        }
    }

    void AbrirMenu()
    {
        SceneManager.LoadScene("Menu1", LoadSceneMode.Additive);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void CerrarMenu()
    {
        SceneManager.UnloadSceneAsync("Menu1");
        Time.timeScale = 1f;
        isPaused = false;
    }
}