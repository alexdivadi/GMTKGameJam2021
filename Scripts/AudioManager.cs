using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public AudioMixer fade;
    public AudioMixerGroup fadeIn;
    public AudioMixerGroup fadeOut;
    public Sound[] sounds;

    public static AudioManager instance;

    private float waitTime;
    private bool looping;

    // Start is called before the first frame update
    void Awake() {
        DontDestroyOnLoad(gameObject);

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        foreach (Sound sound in sounds) {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.loop = sound.loop;
        }
    }

    public void Start() {
        instance.playSound("musicIntro");
        waitTime = instance.sounds[15].clip.length;
        looping = false;
    }

    void Update() {
        if (waitTime > 0f)
            waitTime -= Time.deltaTime;
        else if (!looping) {
            instance.playSound("musicLoop");
            looping = true;
        }
            
        
    }

    public void playSound(string name) {
        Sound sound = Array.Find(sounds, x => x.name == name);
        if (sound != null) {
            sound.source.Play();
            if (sound.fadeIn) {
                StartCoroutine(StartFade(sound, 3f, true));
            }
        }
    }

    public void stopSound(string name) {
        Sound sound = Array.Find(sounds, x => x.name == name);
        if (sound != null) {
            sound.source.Stop();
        }
    }

    public IEnumerator StartFade(Sound sound, float duration, bool fadingIn) {
        float targetVolume = 0f;
        string exposedParam = "";

        if (fadingIn) {
            sound.source.outputAudioMixerGroup = fadeIn;
            targetVolume = 1f;
            exposedParam = "fadeIn";
        } else {
            sound.source.outputAudioMixerGroup = fadeOut;
            exposedParam = "fadeOut";
        }

        float currentTime = 0;
        float currentVolume;
        fade.GetFloat(exposedParam, out currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);

        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
            fade.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            yield return null;
        }

        yield break;
    }
}
