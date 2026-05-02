using UnityEngine;

public class NPCcontroller : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetBool("hablar", true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            this.GetComponent<Animator>().SetBool("hablar", false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
