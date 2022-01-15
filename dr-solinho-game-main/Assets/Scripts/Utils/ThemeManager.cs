﻿using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioListener))]
[RequireComponent(typeof(AudioSource))]
public class ThemeManager : MonoBehaviour
{
    public static ThemeManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.ignoreListenerVolume = true;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private AudioSource m_AudioSource;
    public AudioMixer m_Mixer;

    private void Start()
    {
        var music = PlayerPrefs.GetFloat("musicVolume", 0.1f);
        var sfx = PlayerPrefs.GetFloat("sfxVolume", 20.0f);

        m_Mixer.SetFloat("musicVolume", music);
        m_Mixer.SetFloat("sfxVolume", sfx);
    }

    public void ChangeMusic(AudioClip clip, float time)
    {
        StartCoroutine(Crossing(clip, time));
    }

    private IEnumerator Crossing(AudioClip clip, float time)
    {
        float elapsedTime = 0.0f;
        float volume = m_AudioSource.volume;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(volume, 0.0f, elapsedTime);
            yield return null;
        }

        elapsedTime = 0.0f;
        m_AudioSource.clip = clip;

        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;
            m_AudioSource.volume = Mathf.Lerp(0.0f, volume, elapsedTime);
            yield return null;
        }

        yield return null;
    }
}