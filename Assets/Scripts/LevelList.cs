using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/LevelList", order =  1)]
public class LevelList : ScriptableObject
{
    public List<string> scenes;
}
