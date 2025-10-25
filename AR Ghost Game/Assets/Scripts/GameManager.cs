using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI References")]
    public Text killCountText;
    public Text instructionsText;
    
    [Header("Game Components")]
    public GhostSpawner ghostSpawner;
    public GhostShooter ghostShooter;
    public ReticleManager reticleManager;
    
    void Start()
    {
        // Find components if not assigned
        if (ghostSpawner == null)
            ghostSpawner = FindObjectOfType<GhostSpawner>();
            
        if (ghostShooter == null)
            ghostShooter = FindObjectOfType<GhostShooter>();
            
        if (reticleManager == null)
            reticleManager = FindObjectOfType<ReticleManager>();
        
        // Set up initial UI
        UpdateUI();
        
        // Show instructions
        if (instructionsText != null)
        {
            instructionsText.text = "Point the reticle at a ghost and tap to shoot!";
        }
    }
    
    void Update()
    {
        // Update UI every frame to show current kill count
        UpdateUI();
    }
    
    void UpdateUI()
    {
        if (killCountText != null && ghostSpawner != null)
        {
            killCountText.text = $"Ghosts Killed: {ghostSpawner.GetKillCount()}";
        }
    }
    
    // Public methods that can be called from UI buttons
    public void ResetGame()
    {
        if (ghostSpawner != null)
        {
            ghostSpawner.ResetKillCount();
        }
        
        Debug.Log("Game reset!");
    }
    
    public void ToggleReticle()
    {
        if (reticleManager != null)
        {
            // This would require adding a method to ReticleManager to check visibility
            // For now, we'll just toggle it
            bool currentVisibility = reticleManager.gameObject.activeInHierarchy;
            reticleManager.SetReticleVisibility(!currentVisibility);
        }
    }
}
