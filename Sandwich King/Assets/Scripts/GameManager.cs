using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int[] starsEarned;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeStars();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeStars()
    {
        int levelCount = SceneManager.sceneCountInBuildSettings;
        starsEarned = new int[levelCount];
    }

    public void SetStarsEarned(int levelIndex, int stars)
    {
        if (levelIndex >= 0 && levelIndex < starsEarned.Length)
        {
            starsEarned[levelIndex] = stars;
            Debug.Log("Stars set for level index " + levelIndex + ": " + stars);
        }
    }

    public int GetStarsEarned(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < starsEarned.Length)
        {
            return starsEarned[levelIndex];
        }
        return 0;
    }
}
