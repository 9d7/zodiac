using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SoundAsset", order =  1)]
public class SoundAsset : ScriptableObject
{
    public string id;
    public List<AudioClip> sounds;
    [Range(0f, 2f)] public float maxPitch = 1.0f;
    [Range(0f, 2f)] public float minPitch = 1.0f;
    [Range(0f, 1f)] public float volume = 1.0f;
    public bool positional = true;
    public AudioMixerGroup mixerGroup;

}
