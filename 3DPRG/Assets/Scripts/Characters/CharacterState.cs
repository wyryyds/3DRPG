using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterState : MonoBehaviour
{
    public CharacteData_SO characteData;
    public Attack_SO attackData;
    [HideInInspector]
    public bool isCritical;
    #region Readfrom Data_SO
    /// <summary>
    /// 生命上限
    /// </summary>
    public int MAX_Health { get => characteData != null ? characteData.MAX_Health : 0; set => characteData.MAX_Health=value; }
    /// <summary>
    /// 当前生命值
    /// </summary>
    public int CurrentHealth { get => characteData.currentHeath > 0 ? characteData.currentHeath : 0; set => characteData.currentHeath = value; }
    /// <summary>
    /// 防御上限
    /// </summary>
    public int BaseDefence { get => characteData != null ? characteData.baseDefence : 0; set => characteData.baseDefence = value; }
    public int CurrentDefence { get => characteData.baseDefence; set => characteData.baseDefence = value; }

    #endregion

    #region Character Combat

    public void TakeDAmage(CharacterState attacker, CharacterState defender)
    {
        var damage =Math.Max( attacker.CurrentDamage() - defender.CurrentDefence,0);
        CurrentHealth = Math.Max(CurrentHealth- damage, 0);
        //TODO UI
        //TODO 升级
    }

    private int CurrentDamage()
    {
        float coreDamage = UnityEngine.Random.Range(attackData.minDamage, attackData.maxDamage);
        if(isCritical)
        {
            coreDamage *= attackData.criticalMultiplier;

        }
        return (int)coreDamage;
    }


    #endregion
}
