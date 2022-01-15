using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private Quests[] m_Quests = new Quests[0];

    [SerializeField]
    private GameObject m_QuestPanel = null;

    [SerializeField]
    private GameObject m_QuestInfoPanel = null;

    private QuestHolder[] m_QuestHolders;


    [SerializeField]
    private Sprite m_QuestComplete = null;


    [SerializeField]
    private GameObject m_QuestHelper = null;

    private Text m_QuestHelperText;


    [SerializeField]
    private GameObject m_Portal = null;

    private void Awake()
    {
        m_QuestHolders = new QuestHolder[m_Quests.Length];

        if(m_Quests.Length > 0)
        {
            for(int i =0; i< m_Quests.Length; i++)
            {
                var quest = Instantiate(m_QuestInfoPanel, m_QuestPanel.transform);
                m_QuestHolders[i] = quest.GetComponent<QuestHolder>();
                m_QuestHolders[i].QuestName.text = m_Quests[i].QuestDescription;
                m_QuestHolders[i].Count = 0;
                m_QuestHolders[i].QuestObjectve.text = "0";
                m_QuestHolders[i].QuestObjectveQtd.text = m_Quests[i].QuestQtd.ToString();
                m_QuestHolders[i].HelperText = m_Quests[i].QuestHelper;

            }
        }
        m_QuestHelperText = m_QuestHelper.GetComponentInChildren<Text>();

        QuestHolder.ShowHelper += ShowHelper;
        QuestHolder.CloseHelper += CloseHelper;

        Actions.Quest += IncreaseCounter;

        NpcController.Quest += IncreaseCounter;


    }

    private void OnDestroy()
    {
        QuestHolder.ShowHelper -= ShowHelper;
        QuestHolder.CloseHelper -= CloseHelper;

        Actions.Quest -= IncreaseCounter;

        NpcController.Quest -= IncreaseCounter;
    }

    private void ShowHelper(string text)
    {
        m_QuestHelperText.text = text;
        m_QuestHelper.SetActive(true);
    }

    private void CloseHelper()
    {
        m_QuestHelper.SetActive(false);
    }

    private void IncreaseCounter(int id)
    {
        for(int i = 0; i < m_Quests.Length; i++)
        {
            if(m_Quests[i].QuestID == id && m_Quests[i].IsActive)
            {
                m_QuestHolders[i].Count++;
                m_QuestHolders[i].QuestObjectve.text = m_QuestHolders[i].Count.ToString();
                if(m_QuestHolders[i].Count >= m_Quests[i].QuestQtd)
                {
                    m_Quests[i].IsActive = false;
                    m_QuestHolders[i].QuestCheck.sprite = m_QuestComplete;
                    AllQuests();
                }

                return;
            }            

        }

    }

    private void AllQuests()
    {
        int count = 0;
        for(int i=0; i<m_Quests.Length; i++)
        {
            if(!m_Quests[i].IsActive)
                count++;
        }

        if(count >= m_Quests.Length)
            m_Portal.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
