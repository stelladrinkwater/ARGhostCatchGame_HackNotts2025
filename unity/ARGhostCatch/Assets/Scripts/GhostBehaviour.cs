using UnityEngine;

public class GhostBehaviour : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [Tooltip("Speed of the bobbing motion")]
    public float bobbingSpeed = 2f;
    
    [Tooltip("Height of the bobbing motion")]
    public float bobbingHeight = 0.5f;
    
    [Header("Rotation Settings")]
    [Tooltip("Speed of ghost rotation (Y-axis only to prevent backflips)")]
    public float rotationSpeed = 30f;
    
    [Header("Physical Anchoring")]
    [Tooltip("The ghost will stay anchored to this physical world position")]
    private Vector3 anchorPosition;
    
    private float bobbingTimer;
    private GhostSpawner spawner;
    
    void Start()
    {
        // Anchor the ghost to its spawn position in physical space
        anchorPosition = transform.position;
        
        // Find the spawner that created this ghost
        spawner = FindObjectOfType<GhostSpawner>();
        
        // Start bobbing timer at random point to avoid synchronized movement
        bobbingTimer = Random.Range(0f, 2f * Mathf.PI);
    }
    
    void Update()
    {
        // Update bobbing motion
        PerformBobbingMotion();
        
        // Rotate the ghost for visual appeal
        PerformRotation();
    }
    
    void PerformBobbingMotion()
    {
        // Increment bobbing timer
        bobbingTimer += Time.deltaTime * bobbingSpeed;
        
        // Calculate vertical offset using sine wave
        float yOffset = Mathf.Sin(bobbingTimer) * bobbingHeight;
        
        // Apply bobbing motion while maintaining anchor position
        transform.position = anchorPosition + new Vector3(0, yOffset, 0);
    }
    
    void PerformRotation()
    {
        // Only rotate around Y-axis to prevent backflips
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Check if hit by player projectile or interaction
        if (other.CompareTag("Player") || other.CompareTag("Projectile"))
        {
            OnGhostHit();
        }
    }
    
    void OnMouseDown()
    {
        // Allow touch/click interaction to shoot ghost
        OnGhostHit();
    }
    
    public void OnGhostHit()
    {
        Debug.Log("Ghost hit and destroyed!");
        
        // Notify spawner that this ghost was shot
        if (spawner != null)
        {
            spawner.OnGhostShot();
        }
        
        // Destroy this ghost
        Destroy(gameObject);
    }
    
    void OnDrawGizmosSelected()
    {
        // Visualize the anchor position in the editor
        Gizmos.color = Color.red;
        Vector3 center = Application.isPlaying ? anchorPosition : transform.position;
        Gizmos.DrawWireSphere(center, 0.1f);
        
        // Show bobbing range
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(center + Vector3.up * bobbingHeight, center - Vector3.up * bobbingHeight);
    }
}
