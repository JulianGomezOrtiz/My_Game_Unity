using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenUI : MonoBehaviour
{
    public TextMeshProUGUI deathText;
    public float fadeDuration = 1.5f;
    public float menuDelay = 0.5f;

    private Health playerHealth;

    void Awake()
    {
        if (deathText == null)
            deathText = GetComponent<TextMeshProUGUI>();

        if (deathText != null)
        {
            deathText.text = "<b>MORISTE</b>";
            deathText.color = new Color(0.8f, 0f, 0f);
            deathText.fontSize = 72;
            deathText.alignment = TextAlignmentOptions.Center;

            RectTransform rt = deathText.rectTransform;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;

            Color c = deathText.color;
            c.a = 0f;
            deathText.color = c;
        }
    }

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        playerHealth = player.GetComponent<Health>();
        if (playerHealth != null)
            playerHealth.OnDied += ShowDeathScreen;
    }

    void ShowDeathScreen()
    {
        if (deathText == null) return;

        StartCoroutine(DeathSequence());
    }

    System.Collections.IEnumerator DeathSequence()
    {
        yield return StartCoroutine(AnimateText());
        yield return new WaitForSecondsRealtime(menuDelay);

        if (!SceneManager.GetSceneByName("Menu1").isLoaded)
        {
            SceneManager.LoadScene("Menu1", LoadSceneMode.Additive);
            Time.timeScale = 0f;
        }
    }

    System.Collections.IEnumerator AnimateText()
    {
        float elapsed = 0f;
        Vector3 startScale = Vector3.one * 0.5f;
        Vector3 endScale = Vector3.one;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float eased = t * t * (3f - 2f * t);

            deathText.transform.localScale = Vector3.Lerp(startScale, endScale, eased);
            deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, eased);

            yield return null;
        }

        deathText.transform.localScale = endScale;
        deathText.color = new Color(deathText.color.r, deathText.color.g, deathText.color.b, 1f);
    }

    void OnDestroy()
    {
        if (playerHealth != null)
            playerHealth.OnDied -= ShowDeathScreen;
    }
}
