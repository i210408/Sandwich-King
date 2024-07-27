using UnityEngine;
using UnityEngine.UI;

public class DiagonalScroll : MonoBehaviour
{
    public RawImage backgroundImage;
    public float scrollSpeedX = 0.1f;
    public float scrollSpeedY = 0.1f;

    private Vector2 uvOffset = Vector2.zero;

    void Update()
    {
        if (backgroundImage == null)
        {
            Debug.LogError("Background Image is not assigned.");
            return;
        }

        // Update the UV offset based on the scroll speed and time
        uvOffset.x += scrollSpeedX * Time.deltaTime;
        uvOffset.y += scrollSpeedY * Time.deltaTime;

        // Wrap the UV offset to keep it within 0 to 1
        uvOffset.x = uvOffset.x % 1;
        uvOffset.y = uvOffset.y % 1;

        // Apply the UV offset to the RawImage
        backgroundImage.uvRect = new Rect(uvOffset.x, uvOffset.y, backgroundImage.uvRect.width, backgroundImage.uvRect.height);
    }
}