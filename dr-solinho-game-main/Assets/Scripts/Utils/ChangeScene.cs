using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public bool m_UseTouch;
    public bool m_UseTimeToChangeScene;
    public float m_Time = 3.0f;
    public string m_SceneName;
    public AudioClip m_Clip;

    public bool m_Used;
    private float m_DefaultSfx = 75.0f;

    public void Start()
    {
        m_DefaultSfx = PlayerPrefs.GetFloat("sfxVolume", m_DefaultSfx);
        if (m_UseTimeToChangeScene)
        {
            Invoke("LoadLevel", m_Time);
        }
    }

    public void Update()
    {
        if (m_UseTouch)
        {
            if (Input.anyKeyDown)
            {
                LoadLevel(m_SceneName);
            }
        }
    }

    public void LoadLevelWithTime()
    {
        if (m_Used)
            return;

        if (m_Clip)
            AudioSource.PlayClipAtPoint(m_Clip, Camera.main.transform.position, m_DefaultSfx);
        m_Used = true;
        ScreenManager.Instance.LoadLevelLoading(m_SceneName);
    }

    public void LoadLevel(string sceneName)
    {
        if (m_Used)
            return;

        if (m_Clip)
            AudioSource.PlayClipAtPoint(m_Clip, Camera.main.transform.position, m_DefaultSfx);
        m_Used = true;
        ScreenManager.Instance.LoadLevel(sceneName);
    }

    public void LoadLevelWithLoading(string sceneName)
    {
        if (m_Used)
            return;

        if (m_Clip)
            AudioSource.PlayClipAtPoint(m_Clip, Camera.main.transform.position, m_DefaultSfx);
        m_Used = true;
        ScreenManager.Instance.LoadLevelLoading(sceneName);
    }
}
