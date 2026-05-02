using UnityEngine;

public class Giro : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        this.transform.Rotate(50f * Time.deltaTime, 50f * Time.deltaTime, 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}
