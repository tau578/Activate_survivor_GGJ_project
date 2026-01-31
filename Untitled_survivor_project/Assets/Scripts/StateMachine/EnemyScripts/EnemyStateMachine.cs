using System.Collections;
using UnityEngine;
using UnityEngine.AI;



public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController CharacterController { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
    [field: SerializeField] public BoxCollider BodyBoxCollider { get; private set; }
    [field: SerializeField] public BoxCollider PunchWarningBox { get; private set; } // useless now, for later might be useful
   // [field: SerializeField] public PunchHitBox PunchHitBox { get; private set; } // useless now, for later might be useful
    [field: SerializeField] public Animator Animator {get; private set; }
    [field: SerializeField] public EnemySoundHandler SoundHandler {get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public float HitImpactDuration { get; private set; }
    [field: SerializeField] public float GettingkickedPushBackForce { get; private set; }
    [field: SerializeField] public Vector2 GuardDurationRange { get; private set; } // how long enemy stays in block state before deciding to attack or chase player
    [field: SerializeField] public float ChaseSpeed { get; private set; } 

    [field: SerializeField] public float PlayerChaseRange { get; private set; }
    [field: SerializeField] public float AttackRange {  get; private set; }
    [field: SerializeField] public float AttackDuration {  get; private set; } 
    [field: SerializeField] public bool HasPotrolState {  get; private set; }

    [Header("Only Set a value if your enemy hasPotrolState")] 
    [field: SerializeField] public float PatrolSpeed { get; private set; } 

    [Header("Only assign way points if your enemy hasPotrolState")] 
    [field: SerializeField] public Transform[] WayPoints {  get; private set; }
    public GameObject Player;
    [HideInInspector] public bool isPlayerKicking; 
    [HideInInspector] public bool didRecentlyPunchPlayer;
    [HideInInspector] public bool wasGaurdRecentlyBroken = false;

    private void OnEnable()
    {
        Health.OnDie += HandleDeath;
    }
    private void OnDisable()
    {
        Health.OnDie -= HandleDeath;
    }
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); 
        Agent.updatePosition = true;
        Agent.updateRotation = true;
        SwitchState(new EnemyIdleState(this));
    }
    private void HandleDeath() // in any state, we want enemy die when health reaches zero
    {
        SwitchState(new EnemyDeadState(this));
    }
    private void OnDrawGizmos() // if you want chase range to always be visiable in the editor, use OnDrawGizmos instead
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(gameObject.transform.position, PlayerChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gameObject.transform.position, AttackRange);
    }
    public IEnumerator GaurdBreakCoolDown() // to prevent enemy from immediately going back to guard state after gaurd is broken
    {
        yield return new WaitForSeconds(3f);
        wasGaurdRecentlyBroken = false;
    }
    public IEnumerator AttackCoolDown() // to prevent enemy from immediately punching player again and again
    {
        yield return new WaitForSeconds(3f);
        didRecentlyPunchPlayer = false;
    }
}