using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyStates { GUARD,PATROL,CHASE,DEAD}
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    //private readonly Collider[] colliders = new Collider[100];

    public EnemyStates enemyStates;
    public float sightRadius;
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
        if (FoundPlayer()) { enemyStates = EnemyStates.CHASE; Debug.Log(111); }
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

    bool FoundPlayer()
    {
        var _colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach(var target in _colliders)
        {
            if (target.CompareTag("Player")) return true;
        }
        return false;
    }
}
