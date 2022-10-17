using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip[] musics;
    public AudioClip[] sounds;

    private static SoundManager instance;

    private AudioSource musicAudio;
    private AudioSource soundFx;

    public static float MusicVolume
    {
        set { instance.musicAudio.volume = value; }
        get { return instance.musicAudio.volume; }
    }

    public static float SoundVolume
    {
        set { instance.soundFx.volume = value; }
        get { return instance.soundFx.volume; }
    }

    void Awake ()
    {
        instance = this;
	}
	
    void Start()
    {
        musicAudio = gameObject.AddComponent<AudioSource>();
        musicAudio.loop = true;
        musicAudio.volume = .5f;
        soundFx = gameObject.AddComponent<AudioSource>();

        PlayMusic(musics[0], musicAudio);

    }

	void Update ()
    {
        if (GlobalValue.isMusic)
            musicAudio.volume = 0;
        else
            musicAudio.volume = 0.5f;

        if (GlobalValue.isSound)
            soundFx.volume = 0;
        else
            soundFx.volume = 1;
    }

    public static void PlaySfx(AudioClip clip)
    {
        instance.PlaySound(clip, instance.soundFx);
    }

    public static void PlaySfx(AudioClip clip, float volume)
    {
       instance.PlaySound(clip, instance.soundFx, volume);
    }

    public static void PlaySfx(string nameSound)
    {
        if(instance == null)
        {
            Debug.Log("No SoundManager Found");
            return;
        }
        instance.PlaySound(nameSound, instance.sounds, instance.soundFx);
    }

    private void PlaySound(string name, AudioClip[] pool, AudioSource audio)
    {
        foreach(AudioClip clip in pool)
        {
            if(clip.name == name)
            {
                PlaySound(clip, audio);
                return;
            }
        }
        Debug.Log("No Audio Found, Check the name");
    }

    public static void PlayMusic(string nameMusic)
    {
        if (instance == null)
        {
            Debug.Log("No SoundManager Found");
            return;
        }
        instance.PlaySound(nameMusic, instance.musics, instance.musicAudio);
    }

    public static void PlayMusic(AudioClip clip)
    {
        instance.PlaySound(clip, instance.musicAudio);
    }

    private void PlayMusic(AudioClip clip, AudioSource audio)
    {
        audio.clip = clip;
        audio.Play();
    }

    private void PlaySound(AudioClip clip, AudioSource audioOut)
    {
        audioOut.PlayOneShot(clip);
    }

    private void PlaySound(AudioClip clip, AudioSource audioOut, float volume)
    {
        audioOut.PlayOneShot(clip, SoundVolume * volume);
    }

}
