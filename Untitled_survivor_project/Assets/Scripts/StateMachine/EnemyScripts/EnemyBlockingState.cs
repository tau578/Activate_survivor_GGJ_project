using UnityEngine;

public class EnemyBlockingState : EnemyBaseState
{
    public EnemyBlockingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    private readonly int blockHash = Animator.StringToHash("Block");
    private float timer;
    private float randomGuardDuration;
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(blockHash, 0f);
        stateMachine.Health.SetVulnerablity(true); // become invulnerable
        stateMachine.isPlayerKicking = false; 
        randomGuardDuration = Random.Range(stateMachine.GuardDurationRange.x, stateMachine.GuardDurationRange.y);
    }
    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        if(timer >= randomGuardDuration)
        {
            Decide();
        }
        if(stateMachine.isPlayerKicking)
        {
            BreakGuard();
        }
    }
    private void Decide()
    {
        if(IsInAttackRange() && !stateMachine.didRecentlyPunchPlayer)
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
    }
    private void BreakGuard()
    {
       // stateMachine.SoundHandler.PlayGuardBreakSound();
        stateMachine.wasGaurdRecentlyBroken = true;
        stateMachine.StartCoroutine(stateMachine.GaurdBreakCoolDown());
        stateMachine.SwitchState(new EnemyImpactState(stateMachine));
    }
    public override void Exit()
    {
        stateMachine.Health.SetVulnerablity(false);
    } 

    
}
