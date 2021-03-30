﻿using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Audio
{
    public string name;

    public AudioClip clip;

    [Range(0.0f, 1.0f)]
    public float volume;

    [Range(0.3f, 3.0f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}