using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField]
    private string NextScene = string.Empty;


    [Header("Agents")]
    [SerializeField]
    private int m_NumebrAgents = 0;

    [SerializeField]
    private GameObject[] m_Agents;


    [SerializeField]
    private bool[] m_HasNavMesh;


    [SerializeField]
    private bool[] m_HasMovement;

    [SerializeField]
    private Transform[] m_Positions;


    [SerializeField]
    private float[] m_Speeds;

    [SerializeField]
    private bool[] m_StartConversations;


    [SerializeField]
    private bool[] m_ContinueConversations;


    [SerializeField]
    private bool[] m_FinishConversations;

    private NavMeshAgent[] m_NavMeshs;


    void Start()
    {

        m_NavMeshs = new NavMeshAgent[m_NumebrAgents];

        for(int i = 0; i < m_NumebrAgents; i++)
        {
            if(m_HasNavMesh[i])
            {
                m_NavMeshs[i] = m_Agents[i].GetComponent<NavMeshAgent>();
            }
        }

            Fungus.FungusPrioritySignals.OnFungusPriorityEnd += EndScene;
    }

    private void OnDestroy()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd -= EndScene;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i =0; i< m_NumebrAgents; i++)
        {
            if(m_HasMovement[i])
            {
                if(m_HasNavMesh[i])
                {
                    m_NavMeshs[i].SetDestination(m_Positions[i].position);
                }
                else
                    m_Agents[i].transform.position = Vector3.MoveTowards(m_Agents[i].transform.position,
                                                                     m_Positions[i].position,
                                                                     m_Speeds[i] *Time.deltaTime);
            }

            if(m_Agents[i].transform.position == m_Positions[i].position && m_StartConversations[i])
            {
                SendMessage();
                m_StartConversations[i] = false;
                m_HasMovement[i] = false;
            }
        }
    }

    private void SendMessage()
    {
        Fungus.Flowchart.BroadcastFungusMessage("Start");
    }

    private void EndScene()
    {
        ScreenManager.Instance.LoadLevelLoading(NextScene);
    }
}
