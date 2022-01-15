using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public string MainScene;
    public string CreditsScene;
    public string SettingsScene;
    public string RankingScene;
    public string StatisticScene;

    private void Update()
    {
        //GameManager.GetDeveloperMode();
    }

    public void NewGame()
    {
        ScreenManager.Instance.LoadLevelLoading(MainScene);
    }

    public void Continue()
    {
        ScreenManager.Instance.LoadLevelLoading(MainScene);
    }

    public void Credits()
    {
        ScreenManager.Instance.LoadLevel(CreditsScene);
    }

    public void Settings()
    {
        ScreenManager.Instance.LoadLevel(SettingsScene);
    }

    public void Rankings()
    {
        ScreenManager.Instance.LoadLevel(RankingScene);
    }

    public void Statistic()
    {
        ScreenManager.Instance.LoadLevel(StatisticScene);
    }

    public void Quit()
    {
        ScreenManager.Instance.QuitGame();
    }


}
