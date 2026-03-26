using UnityEngine;

public class RotarMoneda : MonoBehaviour
{
    public float velocidad = 120f;

    void Update()
    {
        transform.Rotate(Vector3.up * velocidad * Time.deltaTime);
    }
}