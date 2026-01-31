using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    private readonly int impactHash = Animator.StringToHash("Impact");
    private float timer;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(impactHash, 0f);
    }
    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        if(timer >= stateMachine.HitImpactDuration)
        {
            /*
            if(ShouldGuard() && !stateMachine.wasGaurdRecentlyBroken)
            {
                stateMachine.SwitchState(new EnemyBlockingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            } */
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }
    public override void Exit()
    {
        
    }
    private bool ShouldGuard() // randomly decide if enemy should guard or not 
    {
        return Random.value < 0.50f; // 50 percent chance of returning true
    }

    
}
