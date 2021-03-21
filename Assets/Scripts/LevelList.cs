using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelList", order =  1)]
public class LevelList : ScriptableObject
{
    public List<SceneAsset> scenes;
}
