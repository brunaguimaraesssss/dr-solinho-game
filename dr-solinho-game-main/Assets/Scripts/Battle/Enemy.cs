using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public static event System.Action<EnemyScript> BattleStart;
    [SerializeField]
    private EnemyScript m_EnemyScript=null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            BattleStart?.Invoke(m_EnemyScript);
            gameObject.SetActive(false);
        }
    }
}
