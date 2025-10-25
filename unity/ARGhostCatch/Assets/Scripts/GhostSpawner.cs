using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    [Header("Ghost Settings")]
    public GameObject ghostPrefab;
    public float spawnDistance = 5f;
    public float spawnHeight = 1f;
    
    [Header("Ghost Rotation")]
    [Tooltip("Rotation in degrees. X = -90 makes the ghost stand upright.")]
    private readonly Vector3 ghostRotationOffset = new Vector3(-90f, 0f, 0f);
    
    private GameObject currentGhost;
    
    void Start()
    {
        // Spawn the first ghost
        SpawnGhost();
    }
    
    void SpawnGhost()
    {
        // Generate random direction (360 degrees around origin)
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        
        // Calculate spawn position: 5m away from origin, 1m in the air, random direction
        Vector3 spawnPosition = new Vector3(
            Mathf.Cos(randomAngle) * spawnDistance,
            spawnHeight,
            Mathf.Sin(randomAngle) * spawnDistance
        );
        
        // Create rotation with -90 degrees on X axis to make ghost stand upright
        Quaternion ghostRotation = Quaternion.Euler(ghostRotationOffset);
        
        // Spawn the ghost with the rotation
        currentGhost = Instantiate(ghostPrefab, spawnPosition, ghostRotation);
        
        // Ensure the rotation is applied correctly by forcing it
        if (currentGhost != null)
        {
            currentGhost.transform.rotation = ghostRotation;
        }
        
        Debug.Log($"Ghost spawned at: {spawnPosition}");
    }
    
    public void OnGhostShot()
    {
        // Called when a ghost is shot
        currentGhost = null;
        
        // Spawn a new ghost after a short delay
        Invoke(nameof(SpawnGhost), 0.5f);
    }
}
