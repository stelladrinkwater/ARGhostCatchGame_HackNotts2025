using UnityEngine;

public class Ghost : MonoBehaviour
{
    [Header("Bobbing Settings")]
    public float bobbingSpeed = 1f;
    public float bobbingHeight = 0.3f;
    
    private Vector3 startPosition;
    private float bobbingTimer;
    
    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        // Simple bobbing up and down
        bobbingTimer += Time.deltaTime * bobbingSpeed;
        float yOffset = Mathf.Sin(bobbingTimer) * bobbingHeight;
        transform.position = startPosition + new Vector3(0, yOffset, 0);
    }
    
    public void OnShot()
    {
        // Notify spawner that this ghost was shot
        GhostSpawner spawner = FindObjectOfType<GhostSpawner>();
        if (spawner != null)
        {
            spawner.OnGhostShot();
        }
        
        // Destroy this ghost
        Destroy(gameObject);
    }
    
    void OnMouseDown()
    {
        // Simple click/tap to shoot
        OnShot();
    }
}
