using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SfxManager : MonoBehaviour
{
    private static GameObject instance = null;
    private static SoundAssetContainer globalSounds = null;
    private static GameObject globalSfxPlayer = null;
    private static DayNightController dnc = null;
    private static AudioSource day;
    private static AudioSource night;

    public SoundAssetContainer sounds;
    public GameObject sfxPlayer;
    void Awake()
    {

        instance = GameObject.Find("SfxManager");
        if (instance != null && instance != gameObject) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            dnc = FindObjectOfType<DayNightController>();
        };

        globalSounds = sounds;
        globalSfxPlayer = sfxPlayer;

        AudioSource[] sources = instance.GetComponents<AudioSource>();
        day = sources[1];
        night = sources[2];

        day.Play();
        night.Play();
        sources[0].Play();
    }

    public static void PlaySound(string id, Vector3 pos = default(Vector3))
    {
        if (instance is null) return;

        foreach (SoundAssetContainer.SoundAsset sound in globalSounds.sounds)
        {
            if (sound.id == id)
            {
                if (sound.sounds.Count == 0) return;

                bool is2d = (pos == Vector3.zero) || !sound.positional;

                GameObject sfx = Instantiate(globalSfxPlayer, pos, Quaternion.identity);
                AudioSource src = sfx.GetComponent<AudioSource>();

                src.clip = sound.sounds[Random.Range(0, sound.sounds.Count)];
                src.pitch = Random.Range(sound.minPitch, sound.maxPitch);
                src.volume = sound.volume;
                src.outputAudioMixerGroup = sound.mixerGroup;
                src.spatialBlend = is2d ? 0.0f : 1.0f;
                src.Play();

                if (sound.dontDestroyOnLoad) DontDestroyOnLoad(sfx);
                return;
            }
        }

    }

    public void Update()
    {
        day.volume = (dnc.dayAmount + 1f) / 2f;
        night.volume = (1f - dnc.dayAmount) / 2f;
    }
}
