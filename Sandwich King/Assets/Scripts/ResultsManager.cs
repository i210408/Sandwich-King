using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    public Image[] stars; // Array to hold the star images

    private int starsEarned = 0;

    void Start()
    {
        // Get the number of stars earned from the previous scene
        StarData starData = FindObjectOfType<StarData>();
        if (starData != null)
        {
            starsEarned = starData.GetStarsEarned();
        }
        else
        {
            Debug.LogError("Could not find the StarData Object.");
        }
        

        // Ensure all stars are initially inactive
        foreach (var star in stars)
        {
            star.gameObject.SetActive(false);
        }

        // Animate the stars based on the number of stars earned
        StartCoroutine(ShowStars());
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