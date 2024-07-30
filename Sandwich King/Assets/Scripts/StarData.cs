using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarData : MonoBehaviour
{
    private int starsEarned = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetStarsEarned(int starsEarned = 0)
    {
        this.starsEarned = starsEarned;
        Debug.Log("Stars changed to: " + this.starsEarned);
    }

    public int GetStarsEarned()
    {
        return starsEarned;
    }
  
}
