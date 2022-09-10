using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInvoker : MonoBehaviour
{
    public Sound sound;

    public void Play()
    {
        SoundManager.PlaySound(sound);
    }

    public void Play(Sound sound)
    {
        SoundManager.PlaySound(sound);
    }

    public void Play(string sound)
    {
        SoundManager.PlaySound(sound);
    }
}
