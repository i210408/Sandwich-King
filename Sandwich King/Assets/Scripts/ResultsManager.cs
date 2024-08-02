using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    public Image[] stars; // Array to hold the star images
    public Button mainMenuButton;
    public Button levelSelectButton;

    private int starsEarned;
    private string currentLevelName;
    private StarData starData;

    void Start()
    {
        starData = FindObjectOfType<StarData>();

        // Get the number of stars earned and the current level name from StarData
        starsEarned = starData.GetStarsEarned();
        currentLevelName = starData.GetCurrentLevelName();

        // Log the current level name for debugging
        Debug.Log("Current level name: " + currentLevelName);

        // Save the stars earned for the current level
        SaveStarsEarned();

        // Ensure all stars are initially inactive
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        // Animate the stars based on the number of stars earned
        StartCoroutine(ShowStars());

        // Assign button click events
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("MainMenu"));
        levelSelectButton.onClick.AddListener(() => SceneManager.LoadScene("LevelSelect"));
    }

    void SaveStarsEarned()
    {
        int previousStars = PlayerPrefs.GetInt(currentLevelName + "_Stars", 0);
        if (starsEarned > previousStars)
        {
            PlayerPrefs.SetInt(currentLevelName + "_Stars", starsEarned);
            Debug.Log("Saved " + starsEarned + " stars for level " + currentLevelName);
        }
        else
        {
            Debug.Log("Kept previous stars (" + previousStars + ") for level " + currentLevelName);
        }
    }

    IEnumerator ShowStars()
    {
        for (int i = 0; i < starsEarned; i++)
        {
            stars[i].gameObject.SetActive(true);
            stars[i].GetComponent<Animator>().SetTrigger("Appear");
            yield return new WaitForSeconds(0.5f); // Delay between each star appearing
        }
    }
}
