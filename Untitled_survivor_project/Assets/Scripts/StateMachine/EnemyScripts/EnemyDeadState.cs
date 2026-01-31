using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public EnemyDeadState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    private float blinkTimer;
    private float disappearTimer;
    private float waitTimer;
    private float timeToWait = 3f;
    private float timeToDisapear = 4f;
    private float timeBetweenEachBlink = 0.5f;
    private SpriteRenderer sprite;
    private readonly int dieHash = Animator.StringToHash("Die");
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(dieHash, 0f);
      //  stateMachine.SoundHandler.PlayBoneBreakSound();
        sprite = stateMachine.GetComponentInChildren<SpriteRenderer>();
        stateMachine.BodyBoxCollider.enabled = false;
    }
    public override void Tick(float deltaTime)
    {
        waitTimer += deltaTime;
        if(waitTimer < timeToWait)
        {
            return;
        }

        HandleBlinking(deltaTime);
        HandleDisappreaing(deltaTime);
    }
    private void HandleBlinking(float deltaTime)
    {
        blinkTimer += deltaTime;
        if(blinkTimer >= timeBetweenEachBlink)
        {
            sprite.enabled = !sprite.enabled;
            MakeItBlinkFaster();
            blinkTimer = 0f; 
        }
    }
    private void HandleDisappreaing(float deltaTime)
    {
        disappearTimer += deltaTime;
        if(disappearTimer >= timeToDisapear)
        {
            stateMachine.gameObject.SetActive(false);
        }
    }
    private void MakeItBlinkFaster()
    {
        if(disappearTimer >= timeToDisapear /3)
        {
            timeBetweenEachBlink = 0.1f;
        }
        else if(disappearTimer >= timeToDisapear/2)
        {
            timeBetweenEachBlink = 0.2f;
        }
    }
    public override void Exit()
    {
        
    }

    
}