using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuButtons : MonoBehaviour
{
    public Button playButton;
    public Button tutorialButton;

    void Start()
    {
        // Add listeners to the buttons
        playButton.onClick.AddListener(OnPlayButtonPressed);
        tutorialButton.onClick.AddListener(OnTutorialButtonPressed);
    }

    void OnPlayButtonPressed()
    {
        Debug.Log("Play button has been pressed.");
    }

    void OnTutorialButtonPressed()
    {
        Debug.Log("Tutorial button has been pressed.");
    }
}