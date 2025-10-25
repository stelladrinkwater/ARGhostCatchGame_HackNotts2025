using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    [Header("Camera Setup Helper")]
    [Tooltip("Click this button in the inspector to automatically find and tag your AR camera")]
    public bool findAndTagCamera = false;
    
    void Start()
    {
        FindAndReportCameras();
    }
    
    void Update()
    {
        if (findAndTagCamera)
        {
            findAndTagCamera = false;
            AutoTagMainCamera();
        }
    }
    
    void FindAndReportCameras()
    {
        Debug.Log("=== CAMERA DETECTION ===");
        
        Camera[] allCameras = FindObjectsOfType<Camera>();
        Debug.Log($"Found {allCameras.Length} camera(s) in scene:");
        
        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            Debug.Log($"✓ MainCamera already tagged: {mainCam.gameObject.name}");
        }
        else
        {
            Debug.LogWarning("⚠ No camera tagged as 'MainCamera'");
        }
        
        foreach (Camera cam in allCameras)
        {
            string tagStatus = cam.CompareTag("MainCamera") ? " (MainCamera ✓)" : $" (Tag: {cam.tag})";
            Debug.Log($"  - {cam.gameObject.name}{tagStatus}");
            
            // Check if it's likely an AR camera
            if (cam.gameObject.name.ToLower().Contains("ar") || 
                cam.gameObject.name.ToLower().Contains("xr") ||
                cam.gameObject.name.ToLower().Contains("main"))
            {
                Debug.Log($"    ^ This looks like your AR camera!");
            }
        }
    }
    
    [ContextMenu("Auto Tag Main Camera")]
    public void AutoTagMainCamera()
    {
        Camera[] allCameras = FindObjectsOfType<Camera>();
        
        if (allCameras.Length == 0)
        {
            Debug.LogError("No cameras found in scene!");
            return;
        }
        
        // If there's already a MainCamera, we're done
        if (Camera.main != null)
        {
            Debug.Log($"MainCamera already exists: {Camera.main.gameObject.name}");
            return;
        }
        
        // Try to find the AR camera
        Camera arCamera = null;
        
        foreach (Camera cam in allCameras)
        {
            string name = cam.gameObject.name.ToLower();
            if (name.Contains("ar") || name.Contains("xr") || name.Contains("main"))
            {
                arCamera = cam;
                break;
            }
        }
        
        // If no AR camera found, use the first camera
        if (arCamera == null)
        {
            arCamera = allCameras[0];
        }
        
        // Tag it as MainCamera
        arCamera.gameObject.tag = "MainCamera";
        Debug.Log($"✓ Tagged '{arCamera.gameObject.name}' as MainCamera");
    }
    
    // Manual method to tag a specific camera
    public void TagCameraAsMain(Camera camera)
    {
        if (camera != null)
        {
            camera.gameObject.tag = "MainCamera";
            Debug.Log($"✓ Tagged '{camera.gameObject.name}' as MainCamera");
        }
    }
}
