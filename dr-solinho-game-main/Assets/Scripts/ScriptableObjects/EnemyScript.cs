using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Create Enemy")]
public class EnemyScript : ScriptableObject
{
    public string Name;
    public float TotalHealth;
    public float AttackPower;
    public GameObject WorldEnemy;
    public GameObject BattleEnemy;

}
