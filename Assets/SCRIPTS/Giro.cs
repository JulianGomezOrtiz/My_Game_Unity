using UnityEngine;

public class Giro : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(50f * Time.deltaTime, 50f * Time.deltaTime, 50f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<AudioSource>().isPlaying) {
            GetComponent<AudioSource>().Play();
        }
    }
}
