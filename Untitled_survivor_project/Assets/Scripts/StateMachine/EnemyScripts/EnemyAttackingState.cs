using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }
    private readonly int punchHash = Animator.StringToHash("Attack");
    private float timer;
    private bool hasAlreadyDealtDamge; 
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(punchHash, 0f);
        Debug.Log("entered attacking state");
       // stateMachine.PunchWarningBox.gameObject.SetActive(true); for later if we want to change attack logic 
        //EnemyPunchWarningDetector detector = stateMachine.Player.GetComponentInChildren<EnemyPunchWarningDetector>();
     //   if(detector != null)
    //    {
   //         detector.ShowWarningSign();
   //     }
        hasAlreadyDealtDamge = false;
    }
    public override void Tick(float deltaTime)
    {
        Debug.Log("attacking state ticking");
        timer += deltaTime;
        if(timer >=  0.6f && IsInAttackRange() && !hasAlreadyDealtDamge) //
        {
            // stateMachine.PunchHitBox.gameObject.SetActive(true); for later if we want to change attack logic 
            Debug.Log("Enemy dealt damage to player");
            stateMachine.Player.GetComponent<Health>().DealDamage(1);
            hasAlreadyDealtDamge = true;
            stateMachine.didRecentlyPunchPlayer = true;
            stateMachine.StartCoroutine(stateMachine.AttackCoolDown());
        }
        if(timer >= stateMachine.AttackDuration)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
        }
        
    }
    public override void Exit()
    {
     //   stateMachine.PunchHitBox.gameObject.SetActive(false);
//        stateMachine.PunchWarningBox.gameObject.SetActive(false);
    }

    
}
