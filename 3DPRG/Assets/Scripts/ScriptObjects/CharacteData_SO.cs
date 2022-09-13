using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Charater Data")]
public class CharacteData_SO : ScriptableObject
{
    [Header("Stats Info")]
    public int MAX_Health;
    public int currentHeath;
    public int baseDefence;
    public int CurrentDefence;
}
