using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject attackTarget;
    private CharacterState characterState;
    private float attackTime;

    private static readonly int speedString=Animator.StringToHash("Speed");
    private static readonly int attackString = Animator.StringToHash("Attack");
    private static readonly int criticalString = Animator.StringToHash("Critical");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        characterState = GetComponent<CharacterState>();
    }

    private void Start()
    {
        MouseManager.Instance.onMouseClicked += MoveToTarget;
        MouseManager.Instance.onEnemyClicked += EventAttack;
    }

    private void Update()
    {
        SwitchAnimations();
        attackTime -= Time.deltaTime;
    }
    public void MoveToTarget(Vector3 target)
    {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }

    private void SwitchAnimations()
    {
        animator.SetFloat(speedString, agent.velocity.sqrMagnitude);
    }
    private void EventAttack(GameObject targetEnemy)
    {
        if (targetEnemy == null) return;
        attackTarget = targetEnemy;
        characterState.isCritical = UnityEngine.Random.value < characterState.attackData.criticalChance;
        StartCoroutine(MoveToAttackTarget());
    }

    IEnumerator MoveToAttackTarget()
    {
        agent.isStopped = false;
        transform.LookAt(attackTarget.transform);
        while(Vector3.Distance(attackTarget.transform.position,transform.position)>1)
        {
            agent.destination = attackTarget.transform.position;
            yield return null;
        }
        agent.isStopped = true;
        if(attackTime<0)
        {
            animator.SetBool(criticalString, characterState.isCritical);
            animator.SetTrigger(attackString);
            attackTime = characterState.attackData.coolDown;
        }
    }

    void Hit()
    {
        var targetStats = attackTarget.GetComponent<CharacterState>();
        targetStats.TakeDAmage(characterState, targetStats);
    }
}
