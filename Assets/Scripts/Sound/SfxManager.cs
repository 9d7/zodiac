using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    private static GameObject instance = null;
    private static SoundAssetContainer globalSounds = null;
    private static GameObject globalSfxPlayer = null;

    public SoundAssetContainer sounds;
    public GameObject sfxPlayer;
    void Awake()
    {
        if (!(instance is null)) Destroy(gameObject);
        instance = gameObject;
        globalSounds = this.sounds;
        globalSfxPlayer = this.sfxPlayer;
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
                return;
            }
        }

    }
}
