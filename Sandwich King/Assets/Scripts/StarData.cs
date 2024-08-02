using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarData : MonoBehaviour
{
    private int starsEarned = 0;
    private string currentLevelName = "";

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetStarsEarned(int starsEarned)
    {
        this.starsEarned = starsEarned;
        Debug.Log("Stars changed to: " + this.starsEarned);
    }

    public int GetStarsEarned()
    {
        return starsEarned;
    }

    public void SetCurrentLevelName(string levelName)
    {
        this.currentLevelName = levelName;
        Debug.Log("Current level name set to: " + this.currentLevelName);
    }

    public string GetCurrentLevelName()
    {
        return currentLevelName;
    }
}
