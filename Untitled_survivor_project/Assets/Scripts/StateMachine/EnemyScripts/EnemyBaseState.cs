using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine) { this.stateMachine = stateMachine; }

    protected bool IsInChaseRange()
    {
        float distanceFromPlayer = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);
        return distanceFromPlayer <= stateMachine.PlayerChaseRange;
    }
    protected bool IsInAttackRange()
    {
        float distanceSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;
        return distanceSqr <= stateMachine.AttackRange * 2;
    }
    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharacterController.Move(motion * deltaTime); // useless now, for later if we want to use CharacterController for movement
    }
    protected void MoveAgent(Vector3 destination, float speed)
    {
        stateMachine.Agent.speed = speed;
        stateMachine.Agent.SetDestination(destination);
    }
    
    
}