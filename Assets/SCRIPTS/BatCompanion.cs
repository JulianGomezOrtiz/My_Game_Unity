using UnityEngine;

public class BatCompanion : MonoBehaviour
{
    [Header("Follow")]
    public Vector3 offset = new Vector3(0f, 2f, -2f);
    public float followSpeed = 4f;

    [Header("Hover")]
    public float hoverAmplitude = 0.4f;
    public float hoverSpeed = 2.5f;

    private Transform player;
    private Vector3 velocity;

    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        if (go != null) player = go.transform;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 targetPos = player.position + player.TransformDirection(offset);
        targetPos.y += Mathf.Sin(Time.time * hoverSpeed + GetInstanceID()) * hoverAmplitude;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1f / followSpeed);
        transform.LookAt(player.position);
    }
}
