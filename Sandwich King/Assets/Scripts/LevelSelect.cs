using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonParent;
    public LevelList levelList; // Reference to the ScriptableObject

    void Start()
    {
        PopulateLevelButtons();
    }

    void PopulateLevelButtons()
    {
        if (levelList == null || buttonPrefab == null || buttonParent == null)
        {
            Debug.LogError("LevelList, buttonPrefab, or buttonParent is not assigned.");
            return;
        }

        Debug.Log("LevelList contains " + levelList.levels.Count + " levels.");

        foreach (var level in levelList.levels)
        {
            if (string.IsNullOrEmpty(level.sceneName))
            {
                Debug.LogError("Scene name is empty or null.");
                continue;
            }

            Debug.Log("Creating button for scene: " + level.sceneName);

            GameObject button = Instantiate(buttonPrefab, buttonParent);
            button.GetComponentInChildren<Text>().text = level.sceneName;

            // Add stars under the level name
            Transform starsParent = button.transform.Find("StarsParent");
            if (starsParent == null)
            {
                Debug.LogError("StarsParent not found for button: " + level.sceneName);
                continue;
            }

            Debug.Log("level sceneName: " + level.sceneName);

            // Retrieve stars earned from PlayerPrefs
            int starsEarned = PlayerPrefs.GetInt(level.sceneName + "_Stars", 0);
            Debug.Log("Stars earned for " + level.sceneName + ": " + starsEarned);

            // Ensure all star images are initially inactive
            for (int i = 0; i < starsParent.childCount; i++)
            {
                starsParent.GetChild(i).gameObject.SetActive(false);
            }

            // Enable the correct number of star images
            for (int i = 0; i < starsEarned; i++)
            {
                if (i < starsParent.childCount)
                {
                    starsParent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    Debug.LogError("Not enough star placeholders for " + level.sceneName);
                }
            }

            string sceneName = level.sceneName; // Capture the current value of sceneName
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
        }
    }

    void LoadLevel(string sceneName)
    {
        Debug.Log("Loading scene: " + sceneName);
        SceneManager.LoadScene(sceneName);
    }
}
