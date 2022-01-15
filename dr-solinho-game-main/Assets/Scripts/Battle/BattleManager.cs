using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_ActivationElements = new List<GameObject>();
    [SerializeField]
    private Text m_EnemyName=null;
    [SerializeField]
    private Slider m_EnemyHealthBar=null;
    [SerializeField]
    private GameObject m_EnemyPanel=null;
    [SerializeField]
    private ParticleSystem m_AttackEffect = null;

    [SerializeField]
    private Color m_NotHitColor = Color.white;

    [SerializeField]
    private GameObject m_BattlePanel = null;

    [SerializeField]
    private float m_TimeToAttack = 1f;

    private ScaleUI m_Scale;

    private bool m_ScaleIn;

    private Image m_EnemyImage;

    public static event System.Action<float> DamagePlayer;
    public static event System.Action CanAttack;

    public static event System.Action BattleEnd;

    private float m_EnemyHealth;

    private int m_Count;

    private float m_EnemyDamage;
    private float m_PlayerDef = 0;

    private GameObject m_Enemy;

    private bool m_IsWaitingToAttack;
    private bool m_IsCoroutineStarted;

    

    void Start()
    {
        Enemy.BattleStart += StartBattle;
        Attacks.Attack += TakingDamege;
        Attacks.Defence += SetPlayerDef;
    }

    private void LateUpdate()
    {
        if(m_IsWaitingToAttack) 
        {
            if(m_TimeToAttack <= 0f)
                CallEnemyAttack();
            else
                Timer();
        }

    }

    private void Timer()
    {
        m_TimeToAttack -= Time.fixedDeltaTime;
    }

    private void OnDestroy()
    {
        Enemy.BattleStart -= StartBattle;
        Attacks.Attack -= TakingDamege;
        Attacks.Defence -= SetPlayerDef;
    }


    private void StartBattle(EnemyScript script)
    {
        SetEnemy(script);
        StartCoroutine(WaitingForFrame());
    }


    private void SetEnemy(EnemyScript enemy)
    {
        m_EnemyName.text = enemy.Name;
        m_EnemyHealth = enemy.TotalHealth;
        m_EnemyHealthBar.maxValue = m_EnemyHealth;
        m_EnemyDamage = enemy.AttackPower;
        UpdateEnemyHealth();

        m_Enemy= Instantiate(enemy.BattleEnemy, m_EnemyPanel.transform);
        m_EnemyImage = m_Enemy.GetComponent<Image>();
    }

    private void UpdateEnemyHealth()
    {
        m_EnemyHealthBar.value = m_EnemyHealth;
    }

    private void TakingDamege(float dam)
    {
        m_EnemyHealth -= dam;
        UpdateEnemyHealth();

        if(m_EnemyHealth <= 0)
            StartCoroutine(BattleStop());
        else
            StartCoroutine(DamageController());
    }

    private void SetPlayerDef(float def)
    {
        m_PlayerDef = def;

        m_IsWaitingToAttack = true;
    }

    private void CallEnemyAttack()
    {
        float rand = Random.Range(0, m_EnemyDamage + 5);

        if(rand >= m_EnemyDamage)
            rand *= 1.3f;

        DamagePlayer?.Invoke(rand - (m_PlayerDef * 1.3f) + 10);
        m_AttackEffect.Stop();
        m_AttackEffect.Play();

        m_IsWaitingToAttack = false;
        CanAttack?.Invoke();
        m_TimeToAttack = 3;

    }

    private IEnumerator WaitingForFrame()
    {

        m_ActivationElements[m_Count].SetActive(true);

        if(!m_ScaleIn)
        {
            m_Scale = m_BattlePanel.GetComponent<ScaleUI>();
            m_Scale.Enable();
            m_ScaleIn = true;
        }
        m_Count++;    
        yield return new WaitForSeconds(0.7f);

        if(m_Count < m_ActivationElements.Count)
            StartCoroutine(WaitingForFrame());
        else
            m_Count = 0;
    }

    IEnumerator DamageController()
    {
        if(!m_IsCoroutineStarted)
        {
            m_IsCoroutineStarted = true;
            m_EnemyImage.color = Color.red;
            yield return new WaitForSeconds(0.3f);
            m_EnemyImage.color = m_NotHitColor;
            for(int i = 0; i < 10; i++)
            {
                m_EnemyImage.enabled = !m_EnemyImage.enabled;
                yield return new WaitForSeconds(0.2f);
            }
            m_EnemyImage.color = Color.white;
            m_IsCoroutineStarted = false;
        }
        m_IsWaitingToAttack = true;
    }

    private IEnumerator BattleStop()
    {
        m_EnemyDamage = 0;
        m_EnemyHealth = 0;
        Destroy(m_Enemy);
        yield return new WaitForSeconds(0.7f);
        m_Scale.Disable();

        yield return new WaitForSeconds(0.7f);

        BattleEnd?.Invoke();
        foreach(GameObject obj in m_ActivationElements)
        {
            obj.SetActive(false);
        }

        CanAttack?.Invoke();
        m_ScaleIn = false;
        m_IsWaitingToAttack = false;
    }

}
