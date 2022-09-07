using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public List<SoundSerialize> registerSounds;
    [HideInInspector] public Dictionary<string, AudioClip> sounds;

    private AudioSource source;

    public void Start()
    {
        Instance = this;
        source = GetComponent<AudioSource>();
        foreach(SoundSerialize sound in registerSounds)
        {
            sounds.Add(sound.sound.ToString(), sound.clip);
        }
    }

    public static void PlaySound(Sound sound)
    {
        PlaySound(sound.ToString());
    }

    public static void PlaySound(string sound)
    {
        if (!Instance.sounds.ContainsKey(sound))
        {
            Debug.LogError($"Not found AudioClip named {sound}");
            return;
        }

        Instance.source.PlayOneShot(Instance.sounds[sound]);
    }
}

public enum Sound
{
    YourTurn,
}

[System.Serializable]
public struct SoundSerialize
{
    public Sound sound;
    public AudioClip clip;
}
