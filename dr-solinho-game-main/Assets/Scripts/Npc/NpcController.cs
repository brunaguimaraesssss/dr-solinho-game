using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController : MonoBehaviour, IGamePlay, IGameStart
{
    private bool m_IsLookLeft;
    private Animator m_Animator;
    private Rigidbody2D m_RigidBD;
    private NavMeshAgent m_Agent;
    [SerializeField]
    private float m_Speedy =0f;

    private float m_Horizontal;

    [SerializeField]
    private bool m_IsQuest =false;

    [SerializeField]
    private int m_QuestID = 0;

    public static event System.Action<int> Quest;


    public void GamePlay()
    {
        if(m_Horizontal > 0 && m_IsLookLeft)
            Flip();
        else if(m_Horizontal < 0 && !m_IsLookLeft)
            Flip();

        Move();
        
    }

    public void Init()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityStart += TalkStart;
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd += TalkEnd;
        ActionsManager.SetHold += SetHold;

        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();

        m_Agent.updateRotation = false;
        m_Agent.updateUpAxis = false;

        m_RigidBD = GetComponent<Rigidbody2D>();
        StartCoroutine(Walk());
    }

    public void Flip()
    {
        float x = transform.localScale.x * -1;

        m_IsLookLeft = !m_IsLookLeft;

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
    }

    public void Move()
    {
        m_RigidBD.velocity = new Vector2(m_Horizontal * m_Speedy, m_RigidBD.velocity.y);

        if(m_Horizontal != 0)
            m_Animator.SetBool("IsWalk", true);
        else
            m_Animator.SetBool("IsWalk", false);
    }

    private IEnumerator Walk()
    {
        int rand = Random.Range(0, 100);

        if(rand < 33)
            m_Horizontal = -1;
        else if(rand < 66)
            m_Horizontal = 0;
        else
            m_Horizontal = 1;

        yield return new WaitForSeconds(3f);
        StartCoroutine(Walk());
    }

    private void OnDestroy()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityStart -= TalkStart;
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd -= TalkEnd;
        ActionsManager.SetHold -= SetHold;
    }

    private void SetHold(bool hold)
    {
        if(hold)
            StopAll();
        else
            StartCoroutine(Walk());
    }

    private void StopAll()
    {
        StopAllCoroutines();
        m_Horizontal = 0;
        m_Animator.SetBool("IsWalk", false);
        m_RigidBD.Sleep();
    }

    private void TalkStart()
    {
        StopAll();
        
    }

    public void SetQuest()
    {
        if(m_IsQuest)
        {
            m_IsQuest = false;
            Quest?.Invoke(m_QuestID);
        }
    }



    private void TalkEnd()
    {   
        if(this.gameObject.activeSelf)
            StartCoroutine(Walk());
    }


}
