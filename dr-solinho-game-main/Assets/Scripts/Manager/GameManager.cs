using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{

    private PauseManager m_Pause;

    private List<IGameStart> m_Init = new List<IGameStart>();
    private List<IGamePlay> m_GamePlay = new List<IGamePlay>();

    

    private bool m_IsHold;
    private bool m_IsInAction;

    void Start()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityStart += TalkStart;
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd += TalkEnd;

        ActionsManager.SetHold += SetHold;
        Enemy.BattleStart += BattleStart;
        BattleManager.BattleEnd += BattleEnd;
        PortalController.Trigger += PortalTrigger;


        m_Pause = FindObjectOfType(typeof(PauseManager)) as PauseManager;

        var init = FindObjectsOfType<MonoBehaviour>().OfType<IGameStart>();

        foreach(IGameStart obj in init)
            m_Init.Add(obj);

        foreach(var obj in m_Init)
            obj.Init();

        var gplay = FindObjectsOfType<MonoBehaviour>().OfType<IGamePlay>();

        foreach(IGamePlay obj in gplay)
            m_GamePlay.Add(obj);

    }

    private void OnDestroy()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityStart -= TalkStart;
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd -= TalkEnd;
        ActionsManager.SetHold -= SetHold;

        Enemy.BattleStart -= BattleStart;
        BattleManager.BattleEnd -= BattleEnd;


        PortalController.Trigger -= PortalTrigger;
    }

    // Update is called once per frame
    void Update()
    {
        Controll();
        GamePlay();
    }

    private void GamePlay()
    {
        if(!m_IsHold && !m_IsInAction)
        {
            foreach(var obj in m_GamePlay)
                obj.GamePlay();
        }
        
    }

    private void Controll()
    {
        if(Input.GetButtonDown("Cancel") && !m_IsHold)
        {
            Pause();
        }

    }

    private void SetHold(bool hold)
    {
        m_IsInAction = hold;
    }


    public void Pause()
    {
        m_IsHold = true;
        Time.timeScale = 0;
        m_Pause.PausePanel();
    }

    private void PortalTrigger(string name)
    {
        m_IsHold = true;
        ScreenManager.Instance.LoadLevelLoading(name);
    }

    public void Unpause()
    {
        m_Pause.ClosePause();
        Time.timeScale = 1;
        m_IsHold = false;
    }

    private void BattleStart(EnemyScript script)
    {
        m_IsHold = true;
        gameObject.SendMessage("SetBattleMusic");
    }

    private void BattleEnd()
    {
        m_IsHold = false;
        gameObject.SendMessage("SetWorldMusic");
    }

    private void TalkStart()
    {
        m_IsHold = true;
    }

    private void TalkEnd()
    {
        m_IsHold = false;
    }
}
