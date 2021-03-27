using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "ScriptableObjects/ActorScriptableObject", order = 1)]
public class ActorScriptableObject : ScriptableObject
{
    public Sprite sprite;
    public string name;
}
