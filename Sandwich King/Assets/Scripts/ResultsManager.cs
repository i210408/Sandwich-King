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

    void Start()
    {
        // Get the number of stars earned from the previous scene
        starsEarned = PlayerPrefs.GetInt("StarsEarned", 0);

        // Ensure all stars are initially inactive
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        // Animate the stars based on the number of stars earned
        StartCoroutine(ShowStars());

        // Assign button click events
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        levelSelectButton.onClick.AddListener(() => SceneManager.LoadScene("Level  Select"));
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