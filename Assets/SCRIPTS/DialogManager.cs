using TMPro;
using UnityEngine;
using System.Collections;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;

    private Coroutine typingCoroutine;
    private string currentFullText = "";
    private bool isTyping = false;

    void Awake()
    {
        Instance = this;
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }

    public void ShowDialog(string text, float charDelay = 0.05f)
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypewriterEffect(text, charDelay));
    }

    public void SkipOrComplete()
    {
        if (!isTyping) return;

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogText.text = currentFullText;
        isTyping = false;
    }

    IEnumerator TypewriterEffect(string fullText, float delay)
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(true);

        isTyping = true;
        currentFullText = fullText;
        dialogText.text = "";

        foreach (char c in fullText)
        {
            dialogText.text += c;
            yield return new WaitForSecondsRealtime(delay);
        }

        isTyping = false;
    }

    public void HideDialog()
    {
        if (dialogPanel != null)
            dialogPanel.SetActive(false);
    }
}
