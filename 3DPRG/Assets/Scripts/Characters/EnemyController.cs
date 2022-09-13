using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyStates { GUARD,PATROL,CHASE,DEAD}
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private GameObject attackTarget;
    private CharacterState characterState;

    private float speed;
    private Vector3 wayPoint;
    private Vector3 guardPos;
    private float lastAttackTime;

    private static readonly int walk = Animator.StringToHash("Walk");
    private static readonly int chase = Animator.StringToHash("Chase");
    private static readonly int follow = Animator.StringToHash("Follow");
    private static readonly int attack_Trriger = Animator.StringToHash("Attack");
    private static readonly int skill_Trriger = Animator.StringToHash("Skill");
    private static readonly int critical_Bool = Animator.StringToHash("Critical");
    private bool isWalk, isChase, isFollow;

    public EnemyStates enemyStates;
    public float sightRadius;
    public bool isGuard;
    public float patrolRange;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterState = GetComponent<CharacterState>();
        speed = navMeshAgent.speed;
        guardPos = transform.position;
    }

    private void Start()
    {
        if (isGuard) enemyStates = EnemyStates.GUARD;
        else { enemyStates = EnemyStates.PATROL;GetNewWayPoint(); }
    }

    private void Update()
    {
        SwitchSates();
        SwitchAnimation(); 
    }

    private void SwitchAnimation()
    {
        animator.SetBool(walk, isWalk);
        animator.SetBool(chase, isChase);
        animator.SetBool(follow, isFollow);
        animator.SetBool(critical_Bool, characterState.isCritical);
    }

    private void SwitchSates()
    {
        if (FoundPlayer()) { enemyStates = EnemyStates.CHASE; }
        switch (enemyStates)
        {
            case EnemyStates.GUARD:
                break;
            case EnemyStates.PATROL:
                PatrolActions();
                break;
            case EnemyStates.CHASE:
                ChaseActions();
                break;
            case EnemyStates.DEAD:
                break;
        }
        lastAttackTime -= Time.deltaTime;
    }

    private bool TargetInAttackRange()
    {
        if (attackTarget == null) return false;
        return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterState.attackData.attackRange;
    }

    private bool TargetInSkillRange()
    {
        if (attackTarget == null) return false;
        return Vector3.Distance(attackTarget.transform.position, transform.position) <= characterState.attackData.skillRange;
    }

    private bool FoundPlayer()
    {
        var _colliders = Physics.OverlapSphere(transform.position, sightRadius);
        foreach(var target in _colliders)
        {
            if (target.CompareTag("Player"))
            { attackTarget = target.gameObject;return true; }
        }
        return false;
    }

    private void ChaseActions()
    {
        
        isWalk = false;
        isChase = true;
        navMeshAgent.speed = speed;
        if (!FoundPlayer())
        {
            isFollow = false;
            navMeshAgent.destination =transform.position;
            enemyStates = isGuard ? EnemyStates.GUARD : EnemyStates.PATROL;
        }
        else
        {
            isFollow = true;
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = attackTarget.transform.position;
        }
        if (TargetInAttackRange())
        {
            isFollow = false;
            navMeshAgent.isStopped = true;
            if (lastAttackTime < 0)
            {
                lastAttackTime = characterState.attackData.coolDown;
                characterState.isCritical = Random.value < characterState.attackData.criticalChance;
                Attack();
            }
        }
    }

    private void Attack()
    {
        transform.LookAt(attackTarget.transform);
        if(TargetInAttackRange())
        {
            animator.SetTrigger(attack_Trriger);
        }
        if (TargetInSkillRange())
        {
            animator.SetTrigger(skill_Trriger);
        }
        Debug.Log(characterState.isCritical);
    }
    private void PatrolActions()
    {
        isChase = false;
        navMeshAgent.speed = speed *0.5f;
        if(Vector3.Distance(wayPoint,transform.position)<=navMeshAgent.stoppingDistance)
        {
            isWalk = false;
            GetNewWayPoint();
        }
        else
        {
            isWalk = true;
            navMeshAgent.destination = wayPoint;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    private void GetNewWayPoint()
    {
        var randomX = Random.Range(-patrolRange, patrolRange);
        var randomZ = Random.Range(-patrolRange, patrolRange);
        Vector3 randomPoint = new Vector3(guardPos.x + randomX, guardPos.y, guardPos.z + randomZ);
        NavMeshHit hit;
        wayPoint = NavMesh.SamplePosition(randomPoint, out hit, patrolRange,1) ? hit.position : transform.position;

    }
}
