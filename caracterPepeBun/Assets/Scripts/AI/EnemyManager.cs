using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : CharacterManager
{
    EnemyLocomotionManager enemyLocomotionManager;
    EnemyAnimatorManager enemyAnimatorManager;
    EnemyStatsManager enemyStatsManager;
    EnemyEffectsManager enemyEffectsManager;

    public NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidBody;
    public State currentState;
    public CharacterStatsManager currentTarget;

    public bool isPerformingAction;

    public float rotationSpeed = 15;
    public float maximumAggroRange = 1.5f;

    [Header("AI Settings")]
    public float detectionRadius = 20;

    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;
    public float currentRecoveryTime = 0;

    [Header("AI Combo Settings")]
    public bool allowAIToPerformCombos;
    public float comboLikelyHood;
    public bool isPhaseShifting;

    protected override void Awake() {
        base.Awake();
        enemyLocomotionManager = GetComponent<EnemyLocomotionManager>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        enemyStatsManager = GetComponent<EnemyStatsManager>();
        enemyEffectsManager = GetComponent<EnemyEffectsManager>();
        enemyRigidBody = GetComponent<Rigidbody>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        backStabCollider = GetComponent<CriticalDamageCollider>();
        navMeshAgent.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody.isKinematic = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleRecoveryTimer();
        HandleStateMachine();

        isUsingLeftHand = enemyAnimatorManager.animator.GetBool("isUsingLeftHand");
        isUsingRightHand = enemyAnimatorManager.animator.GetBool("isUsingRightHand");
        isRotatingWithRootMotion = enemyAnimatorManager.animator.GetBool("isRotatingWithRootMotion");
        isInteracting = enemyAnimatorManager.animator.GetBool("isInteracting");
        isPhaseShifting = enemyAnimatorManager.animator.GetBool("isPhaseShifting");
        isInvulnerable = enemyAnimatorManager.animator.GetBool("isInvulnerable");
        canDoCombo = enemyAnimatorManager.animator.GetBool("canDoCombo");
        canRotate = enemyAnimatorManager.animator.GetBool("canRotate");
        enemyAnimatorManager.animator.SetBool("isDead", enemyStatsManager.isDead);
    }

    protected override void FixedUpdate() {
        enemyEffectsManager.HandleAllBuildUpEffects();
    }

    private void LateUpdate()
    {
        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleStateMachine()
    {
        if (enemyStatsManager.isDead){
            return;
        }
        
        if(currentState != null){
            State nextState = currentState.Tick(this, enemyStatsManager, enemyAnimatorManager);

            if (nextState != null){
                SwitchToNextState(nextState);
            }
        }
        /*
        if (enemyLocomotionManager.currentTarget != null)
        {
            enemyLocomotionManager.distanceFromTarget = Vector3.Distance(enemyLocomotionManager.currentTarget.transform.position, transform.position);

        }
        if (enemyLocomotionManager.currentTarget == null)
        {
            enemyLocomotionManager.HandleDetection();
        }
        else if (enemyLocomotionManager.distanceFromTarget > enemyLocomotionManager.stoppingDistance)
        {
            enemyLocomotionManager.HandleMoveToTarget();
        }
        else if (enemyLocomotionManager.distanceFromTarget <= enemyLocomotionManager.stoppingDistance)
        {
            AttackTarget();
        }*/
    }

    private void SwitchToNextState(State state){
        currentState = state;
    }

    private void HandleRecoveryTimer(){
        if(currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if(isPerformingAction){
            if (currentRecoveryTime <= 0){
                isPerformingAction = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //replace red with whatever color you prefer
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maximumDetectionAngle, transform.up) * transform.forward * detectionRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(minimumDetectionAngle, transform.up) * transform.forward * detectionRadius;
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);
    }
}
