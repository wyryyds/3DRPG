using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Data",menuName ="Attack Data")]
public class Attack_SO : ScriptableObject
{
    public int attackRange;
    public int skillRange;
    public float coolDown;
    public int minDamage;
    public int maxDamage;
    //±©»÷¼Ó³É
    public float criticalMultiplier;
    public float criticalChance;
}
