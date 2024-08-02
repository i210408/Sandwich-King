using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelList", menuName = "ScriptableObjects/LevelList", order = 1)]
public class LevelList : ScriptableObject
{
    public List<LevelInfo> levels;
}

[System.Serializable]
public class LevelInfo
{
    public string sceneName;
    public int starsEarned;
}
