using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [SerializeField]
    private float floatAmplitude = 0.5f;

    [SerializeField]
    private float floatFrequency = 1f;

    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // simple floating animation
        float yOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = startPos + Vector3.up * yOffset;
        // optional: face the camera
        transform.LookAt(Camera.main.transform);
    }

    // You could also add OnTriggerEnter or OnTap logic for "caught"
}