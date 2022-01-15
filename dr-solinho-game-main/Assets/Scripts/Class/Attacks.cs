using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attacks : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem m_AttackEffect=null;
    [SerializeField]
    private float m_AttackMagntude = 0;

    public static event System.Action<float> Attack;
    public static event System.Action<float> Defence;

    private bool m_CanAttack=true;

    private AudioClip m_ErrorSFX;
    private AudioSource m_AudioSource;

    private void Start()
    {
        BattleManager.CanAttack += CanAttack;
        m_AudioSource = GetComponent<AudioSource>();
        m_ErrorSFX = Resources.Load("errorSFX") as AudioClip;
    }
    private void OnDestroy()
    {

        BattleManager.CanAttack -= CanAttack;
    }

    private void CanAttack() =>
        m_CanAttack = true;


    public void CallAttack()
    {
        if(m_CanAttack)
            Attack?.Invoke(m_AttackMagntude);
        else
            Failed();
    }


    public void CallDefence() 
    {
        if(m_CanAttack)
            Defence?.Invoke(m_AttackMagntude);
        else
            Failed();
    }

    public void ActiveteEffect()
    {
        if(m_CanAttack)
        {

            m_AttackEffect.Stop();
            m_AttackEffect.Play();
            //StartCoroutine(DeactivateEffect());
            m_CanAttack = false;
        }
    }

    private void Failed() =>
       m_AudioSource.PlayOneShot(m_ErrorSFX);



    //private IEnumerator DeactivateEffect()
    //{
    //    yield return new WaitForSeconds(0.7f);

    //    m_AttackEffect.SetActive(false);

    //}


    


}

