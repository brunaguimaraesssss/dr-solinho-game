using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSceneMusic : MonoBehaviour
{
    public AudioClip SceneMusic;
    public AudioClip BattleMusic;
    public AudioSource AudioSource;
    [SerializeField]
    private float m_DefaultVolume = 10f;

    void Start()
    {
        AudioSource = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioSource>();
        //m_DefaultVolume = PlayerPrefs.GetFloat("musicVolume", m_DefaultVolume);

        AudioSource.volume = m_DefaultVolume;
        AudioSource.clip = SceneMusic;
        AudioSource.loop = true;
        AudioSource.Play();
    }

    private void SetBattleMusic()
    {
        AudioSource.clip = BattleMusic;
        AudioSource.Play();
    }

    private void SetWorldMusic()
    {
        AudioSource.clip = SceneMusic;
        AudioSource.Play();
    }

}
