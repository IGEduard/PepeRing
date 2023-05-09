using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStanceState : State
{
    public AttackState attackState;
    public EnemyAttackAction[] enemyAttacks;
    public PursueTargetState pursueTargetState;

    protected bool randomDestinationSet = false;
    protected float verticalMovementValue = 0;
    protected float horizontalMovementValue = 0;

    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        // check for attack range
        // potentially circle player or walk around them
        // if in attack range return attack state
        // if we are in a cooldown after attacking, return this state and continue circling player
        // if the player runs out of range return the pursue target state

        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        enemyAnimatorManager.animator.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
        enemyAnimatorManager.animator.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
        attackState.hasPerformedAttack = false;

        if (enemyManager.isInteracting)
        {
            enemyAnimatorManager.animator.SetFloat("Vertical", 0);
            enemyAnimatorManager.animator.SetFloat("Horizontal", 0);
            return this;
        }
            

        if (distanceFromTarget > enemyManager.maximumAggroRange)
        {
            return pursueTargetState;
        }

        if (!randomDestinationSet){
            randomDestinationSet = true;
            DecideCirclingAction(enemyAnimatorManager);
        }

        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null)
        {
            randomDestinationSet = false;
            return attackState;
        }
        else
        {
            GetNewAttack(enemyManager);
            
        }
        return this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected void HandleRotateTowardsTarget(EnemyManager enemyManager)
    {
        if (enemyManager.isPerformingAction)
        { // rotate manually
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
        else
        { // rotate with pathfinding (navmesh)
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidBody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }

    protected void DecideCirclingAction(EnemyAnimatorManager enemyAnimatorManager){
        // circle with only forward vertical movement
        // circle with running
        // circle with walking only
        WalkAroundTarget(enemyAnimatorManager);
    }

    protected void WalkAroundTarget(EnemyAnimatorManager enemyAnimatorManager)
    {
        /*verticalMovementValue = Random.Range(0, 1); // only forward motion
        if (verticalMovementValue <= 1 && verticalMovementValue > 0)
        {
            verticalMovementValue  = 0.5f;
        }
        else if ( verticalMovementValue >= -1 && verticalMovementValue < 0)
        {
            verticalMovementValue = -0.5f;
        }*/

        verticalMovementValue = 0.5f;

        horizontalMovementValue = Random.Range(-1, 1);

        if (horizontalMovementValue <= 1 && horizontalMovementValue > 0)
        {
            horizontalMovementValue = 0.5f;
        }
        else if (horizontalMovementValue >= -1 && horizontalMovementValue < 0){
            horizontalMovementValue = -0.5f;
        }
    }

    protected virtual void GetNewAttack(EnemyManager enemyManager)
    {
        Vector3 targetsDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.Angle(targetsDirection, enemyManager.transform.forward);
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

        HandleRotateTowardsTarget(enemyManager);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeedenToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack && distanceFromTarget >= enemyAttackAction.minimumDistanceNeedenToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (attackState.currentAttack != null)
                    {
                        return;
                    }

                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore > randomValue)
                    {
                        attackState.currentAttack = enemyAttackAction;
                    }
                }
            }
        }
    }
}
