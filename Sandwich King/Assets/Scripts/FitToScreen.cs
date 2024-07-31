using UnityEngine;

public class FitToScreen : MonoBehaviour
{
    public Camera mainCamera; // The camera with orthographic projection

    void Start()
    {
        ResizeObjectToFitScreen();
    }

    void ResizeObjectToFitScreen()
    {
        if (mainCamera != null)
        {
            // Get the orthographic size and aspect ratio of the camera
            float orthographicSize = mainCamera.orthographicSize;
            float aspectRatio = (float)Screen.width / Screen.height;

            // Calculate the camera's height and width in world units
            float cameraHeight = 2 * orthographicSize;
            float cameraWidth = cameraHeight * aspectRatio;

            // Get the object's original size in local scale
            Vector3 objectSize = GetObjectSize();
            float objectWidth = objectSize.x;
            float objectHeight = objectSize.y;

            // Calculate the scale factors needed to fit the object within the camera view
            float scaleX = cameraWidth / objectWidth;
            float scaleY = cameraHeight / objectHeight;

            // Set the local scale of the GameObject to fit within the screen
            float scale = Mathf.Min(scaleX, scaleY); // Maintain aspect ratio
            transform.localScale = new Vector3(scale, scale, 1f);
        }
        else
        {
            Debug.LogWarning("Main camera is not assigned.");
        }
    }

    Vector3 GetObjectSize()
    {
        Vector3 size = Vector3.zero;
        size.Scale(transform.localScale);
        return size;
    }
}
