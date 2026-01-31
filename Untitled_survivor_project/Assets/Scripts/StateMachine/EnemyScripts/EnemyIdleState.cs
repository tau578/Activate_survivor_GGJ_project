using UnityEngine;


public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) { }

    private readonly int idleHash = Animator.StringToHash("Idle");
    
    public override void Enter()
    {
        if(stateMachine.HasPotrolState)
        {
            stateMachine.SwitchState(new EnemyPatrolState(stateMachine));
        }
        stateMachine.Animator.CrossFadeInFixedTime(idleHash, 0f);
        stateMachine.Health.OnHealthChanged += HandleTakeDamage;
    }
    public override void Tick(float deltaTime)
    {
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));  // swicht to chasing state when in chase range
        }
    }
    private void HandleTakeDamage(bool wasHealthDecreased)
    {
        stateMachine.SwitchState(new EnemyImpactState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.Health.OnHealthChanged -= HandleTakeDamage;
    }

    
}