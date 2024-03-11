using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound {
    public AudioClip clip;

    public string name;

    [Range(0f, 1f)]
    public float volume = 1f;
    public bool loop;
    public bool fadeIn;
    
    [HideInInspector]
    public AudioSource source;
}
