using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] Sound[] musicSounds;
    [SerializeField] Sound[] sfxSounds;
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    static AudioManager instance;
    readonly float MUSIC_DELAY = 1.5f;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(AudioManager).Name;
                    instance = obj.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayMusicByScene(SceneManager.GetActiveScene());
    }

    private void PlayMusicByScene(Scene scene)
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
        PlayMusic(scene.name + "Music", looping: true);
    }

    private void SceneManager_activeSceneChanged(Scene previousScene, Scene newScene)
    {
        musicSource.Stop();
        PlayMusicByScene(newScene);
    }

    public void PlayMusic(string name, bool looping)
    {
        Sound sound = Array.Find(musicSounds, s => s.name == name);

        if (sound != null)
        {
            musicSource.clip = sound.clip;
            musicSource.loop = looping;
            musicSource.PlayDelayed(MUSIC_DELAY);
        }
        else Debug.LogError("Sound not found!");
    }

    public void PlaySFX(string name, [Optional] bool looping)
    {
        Sound sound = Array.Find(sfxSounds, s => s.name == name);

        if (sound != null)
        {
            sfxSource.clip = sound.clip;
            sfxSource.loop = looping;
            sfxSource.Play();
        }
        else Debug.LogError("Sound not found!");
    }

    public void ToggleMusic() => musicSource.mute = !musicSource.mute;

    public void ToggleSFX() => sfxSource.mute = !sfxSource.mute;

    public void AdjustMusicVolume(float volume) => musicSource.volume = volume;

    public void AdjustSFXVolume(float volume) => sfxSource.volume = volume;
}
