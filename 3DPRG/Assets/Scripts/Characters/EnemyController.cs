using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyStates { GUARD,PATROL,CHASE,DEAD}
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public EnemyStates enemyStates;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        SwitchSates();
    }

    void SwitchSates()
    {
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                break;
            case EnemyStates.CHASE:
                break;
            case EnemyStates.DEAD:
                break;
        }
    }
}
