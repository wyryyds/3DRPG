using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private static readonly int speedId=Animator.StringToHash("Speed");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        MouseManager.Instance.onmouseclicked += MoveToTarget;
    }
    private void Update()
    {
        SwitchAnimations();
    }
    public void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }

    private void SwitchAnimations()
    {
        animator.SetFloat(speedId, agent.velocity.sqrMagnitude);
    }
}
