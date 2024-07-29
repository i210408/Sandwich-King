using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    // Public variable to hold the name of the scene to load
    public string sceneName;

    // Method to load the assigned scene
    public void GoToMenu()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not assigned.");
        }
    }
}