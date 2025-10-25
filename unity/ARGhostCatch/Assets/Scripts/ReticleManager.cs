using UnityEngine;
using UnityEngine.UI;

public class ReticleManager : MonoBehaviour
{
    [Header("Reticle Settings")]
    public Sprite reticleSprite;
    public float reticleSize = 50f;
    public Color reticleColor = Color.white;
    
    private Canvas reticleCanvas;
    private GameObject reticleObject;
    private Image reticleImage;
    
    void Start()
    {
        CreateReticleUI();
    }
    
    void CreateReticleUI()
    {
        // Create Canvas for the reticle
        GameObject canvasGO = new GameObject("ReticleCanvas");
        reticleCanvas = canvasGO.AddComponent<Canvas>();
        reticleCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        reticleCanvas.sortingOrder = 100; // Ensure it's on top
        
        // Add CanvasScaler for responsive scaling
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1080, 1920);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;
        
        // Add GraphicRaycaster (optional, for UI interactions)
        canvasGO.AddComponent<GraphicRaycaster>();
        
        // Create reticle GameObject
        reticleObject = new GameObject("Reticle");
        reticleObject.transform.SetParent(canvasGO.transform, false);
        
        // Add Image component
        reticleImage = reticleObject.AddComponent<Image>();
        
        // Set the reticle sprite
        if (reticleSprite != null)
        {
            reticleImage.sprite = reticleSprite;
        }
        else
        {
            // Load the reticle.png from Assets folder
            reticleSprite = Resources.Load<Sprite>("reticle");
            if (reticleSprite == null)
            {
                Debug.LogWarning("Reticle sprite not found! Make sure reticle.png is in a Resources folder or assign it in the inspector.");
                // Create a simple circle as fallback
                CreateFallbackReticle();
            }
            else
            {
                reticleImage.sprite = reticleSprite;
            }
        }
        
        // Set reticle properties
        reticleImage.color = reticleColor;
        reticleImage.raycastTarget = false; // Don't block raycasts
        
        // Position at center and set size
        RectTransform rectTransform = reticleObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;
        rectTransform.sizeDelta = new Vector2(reticleSize, reticleSize);
        
        Debug.Log("Reticle UI created and centered on screen");
    }
    
    void CreateFallbackReticle()
    {
        // Create a simple white circle texture as fallback
        Texture2D circleTexture = new Texture2D(64, 64, TextureFormat.RGBA32, false);
        Color[] pixels = new Color[64 * 64];
        
        Vector2 center = new Vector2(32, 32);
        float radius = 28f;
        float innerRadius = 24f;
        
        for (int y = 0; y < 64; y++)
        {
            for (int x = 0; x < 64; x++)
            {
                Vector2 pos = new Vector2(x, y);
                float distance = Vector2.Distance(pos, center);
                
                if (distance <= radius && distance >= innerRadius)
                {
                    pixels[y * 64 + x] = Color.white;
                }
                else
                {
                    pixels[y * 64 + x] = Color.clear;
                }
            }
        }
        
        circleTexture.SetPixels(pixels);
        circleTexture.Apply();
        
        Sprite fallbackSprite = Sprite.Create(circleTexture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f));
        reticleImage.sprite = fallbackSprite;
    }
    
    // Public methods to control reticle
    public void SetReticleVisibility(bool visible)
    {
        if (reticleCanvas != null)
            reticleCanvas.gameObject.SetActive(visible);
    }
    
    public void SetReticleColor(Color color)
    {
        reticleColor = color;
        if (reticleImage != null)
            reticleImage.color = color;
    }
    
    public void SetReticleSize(float size)
    {
        reticleSize = size;
        if (reticleObject != null)
        {
            RectTransform rectTransform = reticleObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(size, size);
        }
    }
}
