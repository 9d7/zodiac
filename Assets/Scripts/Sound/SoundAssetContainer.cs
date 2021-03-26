using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundAssetContainer", order =  1)]
public class SoundAssetContainer : ScriptableObject
{
    public List<SoundAsset> sounds;
}
