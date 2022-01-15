using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{

    
    public string MainScene;

    public bool m_AutoSave;
    public AudioMixer m_Mixer;

    [Header("Panels")]
    public GameObject ControllPanel;
    public GameObject AudioPanel;
    public GameObject HubCtrl;
    public GameObject PuzzleCtrl;
    public GameObject FightCtrl;

    [Header("Default AUdio")]
    public float m_DefaultSfx = 75.0f;
    public float m_DefaultMusic = 0.5f;

    [Header("Audio UI")]
    public Slider m_SfxSlider;
    public Slider m_MusicSlider;

    private void Start()
    {
        Load();
        SetMusicVolume();
        SetSfxVolume();
    }

    public void Load()
    {
        m_SfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", m_DefaultSfx);
        m_MusicSlider.value = PlayerPrefs.GetFloat("musicVolume", m_DefaultMusic);
    }

    public void Reset()
    {
        m_SfxSlider.value = m_DefaultSfx;
        SetSfxVolume();

        m_MusicSlider.value = m_DefaultMusic;
        SetMusicVolume();
    }
    


    public void SetMusicVolume()
    {
        m_Mixer.SetFloat("musicVolume", m_MusicSlider.value);
    }

    public void SetSfxVolume()
    {
        m_Mixer.SetFloat("sfxVolume", m_SfxSlider.value);
    }

    private void OnDisable()
    {
        if(m_AutoSave)
            Save();
    }

    public void Save()
    {
        float musicVolume = m_DefaultMusic;
        float sfxVolume = m_DefaultSfx;
        m_Mixer.GetFloat("musicVolume", out musicVolume);
        m_Mixer.GetFloat("sfxVolume", out sfxVolume);

        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }


    public void ShowControlls()
    {
        ControllPanel.SetActive(true);
    }

    public void ShowAudio()
    {
        AudioPanel.SetActive(true);
    }

    public void ShowHubCtrl()
    {
        if(PuzzleCtrl.activeSelf)
            PuzzleCtrl.SetActive(false);
        if(FightCtrl.activeSelf)
            FightCtrl.SetActive(false);

        HubCtrl.SetActive(true);
    }

    public void ShowPuzzleCtrl()
    {
        if(HubCtrl.activeSelf)
            HubCtrl.SetActive(false);
        if(FightCtrl.activeSelf)
            FightCtrl.SetActive(false);

        PuzzleCtrl.SetActive(true);
    }

    public void ShowFightCtrl()
    {
        if(PuzzleCtrl.activeSelf)
            PuzzleCtrl.SetActive(false);
        if(HubCtrl.activeSelf)
            HubCtrl.SetActive(false);

        FightCtrl.SetActive(true);
    }

    public void CloseControll()
    {
        if(PuzzleCtrl.activeSelf)
            PuzzleCtrl.SetActive(false);
        if(FightCtrl.activeSelf)
            FightCtrl.SetActive(false);
        if(HubCtrl.activeSelf)
            HubCtrl.SetActive(false);

        ControllPanel.SetActive(false);
    }

    public void CloseAudio()
    {
        AudioPanel.SetActive(false);
    }

    public void BackToMain()
    {
        ScreenManager.Instance.LoadLevel(MainScene);
    }
}