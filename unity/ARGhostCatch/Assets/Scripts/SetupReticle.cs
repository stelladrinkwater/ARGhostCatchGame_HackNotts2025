using UnityEngine;

[System.Serializable]
public class SetupReticle : MonoBehaviour
{
    [Header("Auto Setup")]
    public bool setupOnStart = true;
    
    [Header("Reticle Configuration")]
    public Sprite customReticleSprite;
    public float reticleSize = 50f;
    public Color reticleColor = Color.white;
    
    void Start()
    {
        if (setupOnStart)
        {
            SetupReticleManager();
        }
    }
    
    public void SetupReticleManager()
    {
        // Check if ReticleManager already exists
        ReticleManager existingManager = FindObjectOfType<ReticleManager>();
        if (existingManager != null)
        {
            Debug.Log("ReticleManager already exists in scene");
            return;
        }
        
        // Create new GameObject for ReticleManager
        GameObject reticleManagerGO = new GameObject("ReticleManager");
        ReticleManager manager = reticleManagerGO.AddComponent<ReticleManager>();
        
        // Configure the manager
        if (customReticleSprite != null)
        {
            manager.reticleSprite = customReticleSprite;
        }
        manager.reticleSize = reticleSize;
        manager.reticleColor = reticleColor;
        
        Debug.Log("ReticleManager created and configured");
        
        // Optionally destroy this setup script after use
        Destroy(this.gameObject, 1f);
    }
}
