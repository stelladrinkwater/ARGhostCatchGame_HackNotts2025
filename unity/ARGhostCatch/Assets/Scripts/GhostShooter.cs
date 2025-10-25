using UnityEngine;
using UnityEngine.InputSystem;

/*
 * SETUP INSTRUCTIONS:
 * 1. Add this script to any GameObject in your scene (e.g., create an empty GameObject called "GhostShooter")
 * 2. Drag your AR Camera to the "Player Camera" field in the inspector
 * 3. (Optional) Drag your GhostSpawner to the "Ghost Spawner" field (will auto-find if not set)
 * 4. Make sure your scene has:
 *    - Ghost objects with either Ghost.cs or GhostBehaviour.cs components
 *    - Colliders on your ghost objects for raycast detection
 *    - A GhostSpawner to handle respawning
 * 
 * HOW IT WORKS:
 * - Point camera at ghost (center of screen/reticle) → Tap screen → Ghost dies if hit
 * - Uses raycast from screen center to detect ghosts
 * - Must aim at the ghost to hit it - missed shots do nothing
 * - Kill count is tracked in the GhostSpawner component
 * 
 * AIMING BEHAVIOR:
 * - Requires precise aiming at the ghost
 * - Raycast from exact screen center (reticle position)
 * - Maximum shooting distance: 50m (configurable)
 * - Visual debug ray in Scene view (red line)
 * - Perfect for skill-based AR gameplay
 */

public class GhostShooter : MonoBehaviour
{
    [Header("Required Components")]
    [Tooltip("Drag your AR Camera here from the scene")]
    public Camera playerCamera;
    
    [Tooltip("Drag your GhostSpawner here from the scene (optional - will auto-find if not set)")]
    public GhostSpawner ghostSpawner;
    
    [Header("Shooting Settings")]
    [Tooltip("Maximum distance for ghost detection")]
    public float maxShootDistance = 50f;
    
    [Header("Debug")]
    public bool showDebugMessages = true;
    public bool showDebugRay = true;
    
    private float lastHeartbeat = 0f;
    
    void Start()
    {
        Debug.Log("=== GhostShooter STARTED! Script is running! ===");
        
        // Only auto-find camera if not assigned in inspector
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
            if (playerCamera == null)
            {
                playerCamera = FindObjectOfType<Camera>();
            }
            
            if (playerCamera != null)
            {
                Debug.Log($"GhostShooter: Auto-found camera: {playerCamera.gameObject.name}");
            }
        }
        else
        {
            Debug.Log($"GhostShooter: Using assigned camera: {playerCamera.gameObject.name}");
        }
        
        // Only auto-find ghost spawner if not assigned in inspector
        if (ghostSpawner == null)
        {
            ghostSpawner = FindObjectOfType<GhostSpawner>();
            if (ghostSpawner != null)
            {
                Debug.Log($"GhostShooter: Auto-found GhostSpawner: {ghostSpawner.gameObject.name}");
            }
        }
        else
        {
            Debug.Log($"GhostShooter: Using assigned GhostSpawner: {ghostSpawner.gameObject.name}");
        }
        
        // Validation
        if (playerCamera == null)
        {
            Debug.LogError("GhostShooter: No camera assigned! Please drag your AR Camera to the 'Player Camera' field in the inspector.");
        }
        
        if (ghostSpawner == null)
        {
            Debug.LogError("GhostShooter: No GhostSpawner found! Please drag your GhostSpawner to the inspector or add one to the scene.");
        }
    }
    
    void Update()
    {
        // Heartbeat message every 5 seconds to confirm Update is running
        if (showDebugMessages && Time.time - lastHeartbeat > 5f)
        {
            Debug.Log("GhostShooter Update() is running... (tap screen to test)");
            lastHeartbeat = Time.time;
        }
        
        // Check for input using new Input System
        bool inputDetected = false;
        
        // Check for mouse click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (showDebugMessages)
            {
                Debug.Log("Mouse click detected (New Input System)!");
            }
            inputDetected = true;
        }
        
        // Check for touch input
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            if (showDebugMessages)
            {
                Debug.Log("Touch detected (New Input System)!");
            }
            inputDetected = true;
        }
        
        // Check for keyboard input as fallback
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
        {
            if (showDebugMessages)
            {
                Debug.Log("Keyboard input detected (New Input System)!");
            }
            inputDetected = true;
        }
        
        // Debug input system info occasionally
        if (showDebugMessages && Time.time - lastHeartbeat > 4.5f)
        {
            bool mousePresent = Mouse.current != null;
            bool touchPresent = Touchscreen.current != null;
            bool keyboardPresent = Keyboard.current != null;
            Debug.Log($"New Input System - Mouse: {mousePresent}, Touch: {touchPresent}, Keyboard: {keyboardPresent}");
        }
        
        if (inputDetected)
        {
            if (showDebugMessages)
            {
                Debug.Log("Input confirmed - shooting at screen center!");
            }
            
            ShootAtScreenCenter();
        }
    }
    
    void ShootAtScreenCenter()
    {
        if (showDebugMessages)
        {
            Debug.Log("=== ShootAtScreenCenter() called ===");
        }
        
        if (playerCamera == null)
        {
            if (showDebugMessages)
            {
                Debug.LogError("No camera found! Cannot shoot.");
            }
            return;
        }
        
        // Create a ray from the center of the screen
        Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
        Ray ray = playerCamera.ScreenPointToRay(screenCenter);
        
        if (showDebugMessages)
        {
            Debug.Log($"Shooting ray from screen center: {screenCenter}");
            Debug.Log($"Ray origin: {ray.origin}, Ray direction: {ray.direction}");
        }
        
        // Debug ray visualization
        if (showDebugRay)
        {
            Debug.DrawRay(ray.origin, ray.direction * maxShootDistance, Color.red, 2f);
        }
        
        // Perform raycast to detect ghosts
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxShootDistance))
        {
            if (showDebugMessages)
            {
                Debug.Log($"Raycast hit: {hit.collider.gameObject.name} at distance {hit.distance}");
            }
            
            // Check if we hit a ghost
            Ghost ghost = hit.collider.GetComponent<Ghost>();
            GhostBehaviour ghostBehaviour = hit.collider.GetComponent<GhostBehaviour>();
            
            if (ghost != null)
            {
                if (showDebugMessages)
                {
                    Debug.Log($"Ghost hit with Ghost.cs: {ghost.gameObject.name}");
                }
                ghost.OnShot();
                
                // Increment kill counter
                if (ghostSpawner != null)
                {
                    ghostSpawner.IncrementKillCount();
                    if (showDebugMessages)
                    {
                        Debug.Log($"Ghost killed! Total kills: {ghostSpawner.GetKillCount()}");
                    }
                }
            }
            else if (ghostBehaviour != null)
            {
                if (showDebugMessages)
                {
                    Debug.Log($"Ghost hit with GhostBehaviour.cs: {ghostBehaviour.gameObject.name}");
                }
                ghostBehaviour.OnGhostHit();
                
                // Increment kill counter
                if (ghostSpawner != null)
                {
                    ghostSpawner.IncrementKillCount();
                    if (showDebugMessages)
                    {
                        Debug.Log($"Ghost killed! Total kills: {ghostSpawner.GetKillCount()}");
                    }
                }
            }
            else
            {
                if (showDebugMessages)
                {
                    Debug.Log($"Hit object '{hit.collider.gameObject.name}' but it's not a ghost.");
                    
                    // Show what components the hit object has
                    Component[] components = hit.collider.GetComponents<Component>();
                    Debug.Log("Components on hit object:");
                    foreach (Component comp in components)
                    {
                        Debug.Log($"  - {comp.GetType().Name}");
                    }
                }
            }
        }
        else
        {
            if (showDebugMessages)
            {
                Debug.Log("No object hit by raycast from screen center - missed!");
            }
        }
    }
    
    // Public method to manually trigger shooting (can be called from UI buttons)
    public void TriggerShoot()
    {
        Debug.Log("TriggerShoot() called manually!");
        ShootAtScreenCenter();
    }
    
    // Test method you can call from inspector
    [ContextMenu("Test Shoot at Center")]
    public void TestShoot()
    {
        Debug.Log("Manual test triggered from inspector!");
        ShootAtScreenCenter();
    }
}
