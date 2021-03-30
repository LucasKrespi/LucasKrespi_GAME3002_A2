using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Audio[] audios;

    void Awake()
    {
        foreach (Audio s in audios)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }
    private void Start()
    {
        Play("background");
    }
    public void Play(string name)
    {
        Audio s = Array.Find(audios, Audio => Audio.name == name);
        if(s != null)
        {
            s.source.Play();
        }
    }

    private void Update()
    {
        
    }
}
