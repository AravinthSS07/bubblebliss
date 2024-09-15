using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAcrossScene : MonoBehaviour
{
    public AudioClip musicClip;

    private AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(gameObject); // Ensure the music persists across scenes

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = musicClip;
        audioSource.Play();
    }
}