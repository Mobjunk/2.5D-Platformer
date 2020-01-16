using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer instance;

    [SerializeField] AudioSource audioSource;

    void Start()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Handles playing a sound effect or song using the name of the file in the resource folder
    /// </summary>
    /// <param name="name">The name of the audio file</param>
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load($"Sounds/{name}") as AudioClip;
        audioSource.clip = clip;
        audioSource.Play();
    }

    /// <summary>
    /// Handles playing a sound effect or song using the Sounds enum
    /// </summary>
    /// <param name="sound">One of the possibities from the Sounds enum</param>
    public void PlaySound(Sounds sound)
    {
        AudioClip clip = Resources.Load($"Sounds/{sound.ToString()}") as AudioClip;
        audioSource.clip = clip;
        audioSource.Play();
    }

    public bool IsPlaying()
    {
        return audioSource.isPlaying;
    }
}
