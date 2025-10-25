using UnityEngine;

public class ShootingDebugger : MonoBehaviour
{
    [Header("Debug Info")]
    public bool showDebugInfo = true;
    
    void Start()
    {
        if (showDebugInfo)
        {
            Debug.Log("=== SHOOTING SYSTEM DEBUG ===");
            
            // Check for GhostShooter
            GhostShooter shooter = FindObjectOfType<GhostShooter>();
            if (shooter != null)
            {
                Debug.Log($"✓ GhostShooter found on: {shooter.gameObject.name}");
            }
            else
            {
                Debug.LogError("✗ GhostShooter NOT found! Add GhostShooter script to a GameObject.");
            }
            
            // Check for GhostSpawner
            GhostSpawner spawner = FindObjectOfType<GhostSpawner>();
            if (spawner != null)
            {
                Debug.Log($"✓ GhostSpawner found on: {spawner.gameObject.name}");
            }
            else
            {
                Debug.LogError("✗ GhostSpawner NOT found!");
            }
            
            // Check for Camera
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                Debug.Log($"✓ Main Camera found: {mainCam.gameObject.name}");
            }
            else
            {
                Camera[] allCameras = FindObjectsOfType<Camera>();
                if (allCameras.Length > 0)
                {
                    Debug.LogWarning($"⚠ No MainCamera tag found, but {allCameras.Length} camera(s) exist:");
                    foreach (Camera cam in allCameras)
                    {
                        Debug.Log($"  - {cam.gameObject.name} (Tag: {cam.tag})");
                    }
                }
                else
                {
                    Debug.LogError("✗ No cameras found in scene!");
                }
            }
            
            // Check for ghosts
            Ghost[] ghosts = FindObjectsOfType<Ghost>();
            GhostBehaviour[] ghostBehaviours = FindObjectsOfType<GhostBehaviour>();
            
            Debug.Log($"Ghost components found: {ghosts.Length} Ghost.cs, {ghostBehaviours.Length} GhostBehaviour.cs");
            
            foreach (Ghost ghost in ghosts)
            {
                Collider col = ghost.GetComponent<Collider>();
                if (col != null)
                {
                    Debug.Log($"✓ Ghost '{ghost.gameObject.name}' has collider: {col.GetType().Name}");
                }
                else
                {
                    Debug.LogError($"✗ Ghost '{ghost.gameObject.name}' missing collider!");
                }
            }
            
            foreach (GhostBehaviour ghostBehaviour in ghostBehaviours)
            {
                Collider col = ghostBehaviour.GetComponent<Collider>();
                if (col != null)
                {
                    Debug.Log($"✓ GhostBehaviour '{ghostBehaviour.gameObject.name}' has collider: {col.GetType().Name}");
                }
                else
                {
                    Debug.LogError($"✗ GhostBehaviour '{ghostBehaviour.gameObject.name}' missing collider!");
                }
            }
        }
    }
    
    void Update()
    {
        // Simple input test
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse button 0 pressed!");
        }
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log($"Touch began at: {touch.position}");
            }
        }
    }
    
    // Manual test method you can call from inspector or other scripts
    [ContextMenu("Test Shooting System")]
    public void TestShootingSystem()
    {
        GhostShooter shooter = FindObjectOfType<GhostShooter>();
        if (shooter != null)
        {
            Debug.Log("Manually triggering shoot...");
            shooter.TriggerShoot();
        }
        else
        {
            Debug.LogError("No GhostShooter found to test!");
        }
    }
}
