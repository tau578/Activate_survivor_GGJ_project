using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{
    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    
    private readonly int walkHash = Animator.StringToHash("Walk");
    
    private int wayPointIndex;
    private int direction = 1; // 1 = forward, -1 = backward

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(walkHash, 0f);
        stateMachine.Agent.SetDestination(stateMachine.WayPoints[wayPointIndex].position);
        
        stateMachine.Health.OnHealthChanged += HandleTakeDamage;
    }
    public override void Tick(float deltaTime)
    {
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }

        if (!stateMachine.Agent.pathPending && stateMachine.Agent.remainingDistance <= 0.1f)
        {
            wayPointIndex += direction;

            if (wayPointIndex == stateMachine.WayPoints.Length - 1 || wayPointIndex == 0)
            {
                direction *= -1; // reverse direction
            }
            MoveAgent(stateMachine.WayPoints[wayPointIndex].position, stateMachine.PatrolSpeed);
        }
        
    }
    public override void Exit()
    {
        stateMachine.Health.OnHealthChanged -= HandleTakeDamage;
    }
    private void HandleTakeDamage(bool wasHealthDecreased)
    {
        stateMachine.SwitchState(new EnemyImpactState(stateMachine));
    }
    
}
