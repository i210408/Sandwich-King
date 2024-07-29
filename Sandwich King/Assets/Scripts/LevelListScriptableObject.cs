using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LevelList", menuName = "ScriptableObjects/LevelList", order = 1)]
public class LevelList : ScriptableObject
{
    public List<SceneReference> levels;
}

[System.Serializable]
public class SceneReference
{
    public string sceneName;
}