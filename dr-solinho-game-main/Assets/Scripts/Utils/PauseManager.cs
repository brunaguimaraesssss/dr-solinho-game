using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour, IGameStart
{
    [Header("Panels")]
    public GameObject ControllPanel;
    public GameObject InventoryPanel;
    public GameObject QuestPanel;
    public GameObject AudioPanel;
    public GameObject Pause;
    

    [Header("Default AUdio")]
    public bool m_AutoSave;
    public AudioMixer m_Mixer;
    public float m_DefaultSfx = 75.0f;
    public float m_DefaultMusic = 50.0f;

    [Header("Audio UI")]
    public Slider m_SfxSlider;
    public Slider m_MusicSlider;

    private CreateMenu m_Menu;

    private void Start()
    {
        m_Menu = FindObjectOfType(typeof(CreateMenu)) as CreateMenu;
    }

    public void PausePanel()
    {
        Pause.SetActive(true);
    }
    public void ClosePause()
    {
        Pause.SetActive(false);
        AudioPanel.SetActive(false);
        ControllPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        Save();
    }

    public void Init()
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
        Pause.SetActive(false);
    }

    public void ShowInventory()
    {
        InventoryPanel.SetActive(true);
        m_Menu.OpenMenu();
        Pause.SetActive(false);
    }

    public void CloseInventory()
    {
        InventoryPanel.SetActive(false);
        PausePanel();
    }

    public void ShowQuest()
    {
        QuestPanel.SetActive(true);
        Pause.SetActive(false);
    }

    public void CloseQuest()
    {
        QuestPanel.SetActive(false);
        PausePanel();
    }

    public void ShowAudio()
    {
        AudioPanel.SetActive(true);
        Pause.SetActive(false);
    }


    public void CloseAudio()
    {
        AudioPanel.SetActive(false);
        PausePanel();
    }
}
