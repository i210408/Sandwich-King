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