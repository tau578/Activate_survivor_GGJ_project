using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    private readonly int walkHash = Animator.StringToHash("Walk");
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(walkHash, 0f);
        stateMachine.Health.OnHealthChanged += HandleTakeDamage;
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }

        if (IsInAttackRange())
        {
            stateMachine.Agent.ResetPath();
            stateMachine.Agent.velocity = Vector3.zero;

            if (!stateMachine.didRecentlyPunchPlayer)
            {
                stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            }
            return;
        }

        MoveAgent(stateMachine.Player.transform.position, stateMachine.ChaseSpeed);
    }
    private void HandleTakeDamage(bool wasHealthDecreased)
    {
        stateMachine.SwitchState(new EnemyImpactState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
        stateMachine.Health.OnHealthChanged -= HandleTakeDamage;
    }



}

