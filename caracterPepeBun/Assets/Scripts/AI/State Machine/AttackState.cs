using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public RotateTowardsTargetState rotateTowardsTargetState;
    public CombatStanceState combatStanceState;
    public PursueTargetState pursueTargetState;
    public EnemyAttackAction currentAttack;

    bool willDoComboOnNextAttack = false;
    public bool hasPerformedAttack = false;

    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        // select of of our many attacks based on attack scores
        // if the selected attack is not able to be used because of bad angle or distance, select a new attack
        // if the attack is viable, stop our movement and attack our target
        // set our recovery timer to the attacks recovery time
        // return the combat stance state

/*
        if(enemyManager.isInteracting && enemyManager.canDoCombo == false){
            return this;
        }
        else if (enemyManager.isInteracting && enemyManager.canDoCombo){
            if (willDoComboOnNextAttack){
                willDoComboOnNextAttack = false;
                enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                
            }
        }
            
        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        HandleRotateTowardsTarget(enemyManager);

        if (enemyManager.isPerformingAction)
        {
            return combatStanceState;
        }
        
        if (currentAttack != null) 
        { 
            // if we are too close to the enemy to perform current attack, get a new attack
            if(distanceFromTarget < currentAttack.minimumDistanceNeedenToAttack)
            {
                return this;
            }
            // if we are close enough to attack, then let us proceed
            else if(distanceFromTarget < currentAttack.maximumDistanceNeededToAttack)
            {
                // if our enemy is within our attacks viewable angle, we attack
                if (viewableAngle <= currentAttack.maximumAttackAngle && viewableAngle >= currentAttack.minimumAttackAngle){
                    if (enemyManager.currentRecoveryTime <= 0 && enemyManager.isPerformingAction == false){
                        enemyAnimatorManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                        enemyAnimatorManager.anim.SetFloat("Horizontal", 0, 0.1f, Time.deltaTime);
                        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
                        enemyManager.isPerformingAction = true;
                        RollForComboChance(enemyManager);

                        if(currentAttack.canCombo && willDoComboOnNextAttack){
                            currentAttack = currentAttack.comboAction;
                            return this;
                        }
                        else {
                            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
                            currentAttack = null;
                            return combatStanceState;
                        }
                        
                    }
                }
            }
        }
        else {
            GetNewAttack(enemyManager);
        }
        return combatStanceState;
*/
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        RotateTowardsTargetWhilstAttacking(enemyManager);

        if (distanceFromTarget > enemyManager.maximumAggroRange){
            return pursueTargetState;
        }

        if (willDoComboOnNextAttack && enemyManager.canDoCombo){
            AttackTargetWithCombo(enemyAnimatorManager, enemyManager);
            
        }

        if (!hasPerformedAttack){
            AttackTarget(enemyAnimatorManager, enemyManager);
            RollForComboChance(enemyManager);
        }

        if (willDoComboOnNextAttack && hasPerformedAttack){
            return this; // goes back up to perform the combo
        }

        return rotateTowardsTargetState;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AttackTarget(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager){
        enemyAnimatorManager.animator.SetBool("isUsingRightHand", currentAttack.isRightHandedAction);
        enemyAnimatorManager.animator.SetBool("isUsingLeftHand", !currentAttack.isRightHandedAction);
        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemyAnimatorManager.PlayWeaponTrailFX();
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        hasPerformedAttack = true;
    }

    private void AttackTargetWithCombo(EnemyAnimatorManager enemyAnimatorManager, EnemyManager enemyManager){
        enemyAnimatorManager.animator.SetBool("isUsingRightHand", currentAttack.isRightHandedAction);
        enemyAnimatorManager.animator.SetBool("isUsingLeftHand", !currentAttack.isRightHandedAction);
        willDoComboOnNextAttack = false;
        enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
        enemyAnimatorManager.PlayWeaponTrailFX();
        enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
        currentAttack = null;
    }

    private void RotateTowardsTargetWhilstAttacking(EnemyManager enemyManager)
    {
/*        if (enemyManager.isPerformingAction){ // rotate manually
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero){
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
        else { // rotate with pathfinding (navmesh)
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidBody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
*/
        if (enemyManager.canRotate && enemyManager.isInteracting)
        { 
            // rotate manually
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero){
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
        }
    }

    private void RollForComboChance(EnemyManager enemyManager)
    {
        float comboChance = Random.Range(0, 100);

        if (enemyManager.allowAIToPerformCombos && comboChance <= enemyManager.comboLikelyHood)
        {
            if (currentAttack.comboAction != null){
                willDoComboOnNextAttack = true;
                currentAttack = currentAttack.comboAction;
            }
            else {
                willDoComboOnNextAttack = false;
                currentAttack = null;
            }
        }
    }
}
