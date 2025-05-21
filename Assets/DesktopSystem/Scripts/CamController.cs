using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CamController : MonoBehaviour {
    private float width, height;
    private float initialWidth, initialHeight;
    public float aspectRatio;
    Camera cam;
    RectTransform windowRectTransform;
    RectTransform rectTransform;
    void Start() {
        cam = Camera.main;
        windowRectTransform = this.transform.parent.parent.GetComponent<RectTransform>();
        rectTransform = this.GetComponent<RectTransform>();
        aspectRatio = windowRectTransform.rect.width / windowRectTransform.rect.height;
        initialWidth = rectTransform.rect.width;
        initialHeight = rectTransform.rect.height;
        width = windowRectTransform.rect.width;
        height = windowRectTransform.rect.height;
    }

    void Update() {
        if (Mathf.Approximately(width, rectTransform.rect.width) &&
            Mathf.Approximately(height, rectTransform.rect.height)) return;
        
        cam.targetTexture = Resize(cam.targetTexture, Mathf.FloorToInt(windowRectTransform.rect.width), Mathf.FloorToInt(windowRectTransform.rect.height));
        width = windowRectTransform.rect.width;
        height = windowRectTransform.rect.height;
        ResizeToFit(width, height);
        cam.aspect = aspectRatio;
    }
    
    RenderTexture Resize(RenderTexture renderTexture, int width, int height) {
        if (renderTexture) {
            renderTexture.Release();
            renderTexture.width = width;
            renderTexture.height = height; 
            renderTexture.Create();
        }
        return renderTexture;
    }
    
    public void ResizeToFit(float maxWidth, float maxHeight)
    {
        // Calculate the aspect ratio from the initial dimensions
        float aspectRatio = initialWidth / initialHeight;

        // Start with the maximum allowed dimensions
        float newWidth = maxWidth;
        float newHeight = maxHeight;

        // Adjust dimensions to maintain the aspect ratio
        if (newWidth / newHeight > aspectRatio)
        {
            // Width is too large; scale it down to fit the height
            newWidth = newHeight * aspectRatio;
        }
        else
        {
            // Height is too large; scale it down to fit the width
            newHeight = newWidth / aspectRatio;
        }

        // Update the RectTransform's sizeDelta to the new dimensions
        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}
