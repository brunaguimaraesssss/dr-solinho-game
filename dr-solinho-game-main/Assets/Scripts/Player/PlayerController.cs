using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IGameStart, IGamePlay
{
    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private Vector3 m_TargetPosition;

    private SpriteRenderer m_SpriteRenderer;

    private string m_NpcName;

    [SerializeField]
    private Color m_Hitcolor = Color.red;
    [SerializeField]
    private Color m_NotHitColor = Color.white;


    private bool m_IsLookLeft;
    private bool m_IsCoroutineStarted;
    private AudioSource m_Audio;


    private bool m_IsLookingUp, m_IsLookingDown, m_isLookingSide, m_IsWalking, m_IsDiagonalUp, m_IsDiagonalDown;


    [SerializeField]
    private float m_Health=0;
    [SerializeField]
    private Slider m_HealthBar=null;

    public float FireDamage;


    public void Init()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityStart += TalkStart;
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd += TalkEnd;
        Enemy.BattleStart += BattleStart;
        BattleManager.DamagePlayer += DamageTaken;
        BattleManager.BattleEnd += BattleEnd;



        m_TargetPosition = transform.position;

        m_Agent = GetComponent<NavMeshAgent>();
        m_Animator = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_Audio = GetComponent<AudioSource>();

        m_Agent.updateRotation = false;
        m_Agent.updateUpAxis = false;
        UpdateHealth();
    }

    private void OnDestroy()
    {
        Fungus.FungusPrioritySignals.OnFungusPriorityStart -= TalkStart;
        Fungus.FungusPrioritySignals.OnFungusPriorityEnd -= TalkEnd;
        Enemy.BattleStart -= BattleStart;
        BattleManager.DamagePlayer -= DamageTaken;
        BattleManager.BattleEnd -= BattleEnd;
    }

    public void GamePlay()
    {
        Controll();
        Move();
        ChangeDirection(SetPos() - transform.position);
        Animation();
    }

    private void Controll()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ClearName();
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit)
            {
                if(hit.collider.CompareTag("Npc"))
                    m_NpcName = hit.collider.gameObject.name;
            }

            GetTarget();
        }
        if(Input.GetMouseButton(1))
        {
            GetTarget();
        }
        
    }

    private void ClearName() => 
        m_NpcName = string.Empty;

    private void GetTarget() => 
        m_TargetPosition = EventSystem.current.IsPointerOverGameObject() ? 
                            transform.position : Camera.main.ScreenToWorldPoint(Input.mousePosition);    

    private void Move()
    {
        m_Agent.SetDestination(m_TargetPosition);

        m_IsWalking = m_Agent.velocity.magnitude > 0.5f;
    }

    private void ChangeDirection(Vector3 pos)
    {
        if(pos.y > 2)
        {
            if(pos.x > 2 || pos.x < -2)
            {
                m_IsLookingUp = false;
                m_IsLookingDown = false;
                m_isLookingSide = false;
                m_IsDiagonalUp = true;
                m_IsDiagonalDown = false;
            }
            else
            {
                m_IsLookingUp = true;
                m_IsLookingDown = false;
                m_isLookingSide = false;
                m_IsDiagonalUp = false;
                m_IsDiagonalDown = false;
            }

        }
        else if(pos.y < -2)
        {
            if(pos.x > 2 || pos.x < -2)
            {
                m_IsLookingUp = false;
                m_IsLookingDown = false;
                m_isLookingSide = false;
                m_IsDiagonalUp = false;
                m_IsDiagonalDown = true;
            }
            else
            {
                m_IsLookingUp = false;
                m_IsLookingDown = true;
                m_isLookingSide = false;
                m_IsDiagonalUp = false;
                m_IsDiagonalDown = false;
            }
        }
        else if(pos.x != 0)
        {
            m_IsLookingUp = false;
            m_IsLookingDown = false;
            m_isLookingSide = true;
            m_IsDiagonalUp = false;
            m_IsDiagonalDown = false;
        }


        if(m_IsLookLeft && pos.x > 0)
            Flip();
        else if(!m_IsLookLeft && pos.x < 0)
            Flip();
    }

    private void Flip()
    {
        float x = gameObject.transform.localScale.x * -1;

        m_IsLookLeft = !m_IsLookLeft;

        gameObject.transform.localScale = new Vector3(x, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
    }

    private void Animation()
    {
        m_Animator.SetBool("IsWalking", m_IsWalking);
        m_Animator.SetBool("IsSide", m_isLookingSide);
        m_Animator.SetBool("IsUp", m_IsLookingUp);
        m_Animator.SetBool("IsDown", m_IsLookingDown);
        m_Animator.SetBool("IsDiagonalUp", m_IsDiagonalUp);
        m_Animator.SetBool("IsDiagonalDown", m_IsDiagonalDown);
    }

    private Vector3 SetPos()
    {
        return new Vector3(m_TargetPosition.x, m_TargetPosition.y, 0);
    }
        
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals(m_NpcName))
        {
            collision.gameObject.GetComponent<NpcController>().SetQuest();
            gameObject.tag = "Talk";
            ClearName();
        }

        if(collision.gameObject.CompareTag("Fire"))
        {
            m_Audio.Play();
            DamageTaken(FireDamage);
            PushBack();
            StartCoroutine(DamageController());

            m_TargetPosition = transform.position;

        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            m_TargetPosition = transform.position;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals(m_NpcName))
        {
            collision.gameObject.GetComponent<NpcController>().SetQuest();
            gameObject.tag = "Talk";
            ClearName();
        }

        if(collision.gameObject.CompareTag("Fire"))
        {
            m_Audio.Play();
            DamageTaken(FireDamage);
            PushBack();
            StartCoroutine(DamageController());

            m_TargetPosition = transform.position;

        }
    }

    void PushBack()
    {
        Vector3 pos = transform.position;

        var dest = SetPos() - pos;
        float x = 0;
        float y = 0;
        if(dest.x < 0)
        {
            x = -0.5f;
        }
        else if(dest.x > 0)
        {
            x = 0.5f;
        }

        if(dest.y < 0)
        {
            y = -0.5f;
        }
        else if(dest.y > 0)
        {
            y = 0.5f;
        }

        transform.position = new Vector2(pos.x - x, pos.y - y);
    }

    IEnumerator DamageController()
    {
        if(!m_IsCoroutineStarted)
        {
            m_IsCoroutineStarted = true;
            m_SpriteRenderer.color = m_Hitcolor;
            yield return new WaitForSeconds(0.3f);
            m_SpriteRenderer.color = m_NotHitColor;
            for(int i = 0; i < 10; i++)
            {
                m_SpriteRenderer.enabled = !m_SpriteRenderer.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            m_SpriteRenderer.color = Color.white;
            m_IsCoroutineStarted = false;
        }
    }

    private void TalkStart()
    {
        m_TargetPosition = transform.position;
        m_IsWalking = false;
        m_Agent.isStopped =true;
        Animation();
    }
    private void TalkEnd()
    {
        m_Agent.isStopped = false;
        ClearName();
        gameObject.tag = "Player";
    }

    private void UpdateHealth()
    {
        m_HealthBar.value = m_Health;
    }

    public float GetHealth()
    {
        return m_Health;
    }

    private void DamageTaken(float dam)
    {
        m_Health -= dam;
        UpdateHealth();
    }

    private void BattleStart(EnemyScript script)
    {
        m_TargetPosition = transform.position;
        m_IsWalking = false;
        m_Agent.isStopped = true;
    }

    private void BattleEnd()
    {
        m_Agent.isStopped = false;
    }

}
