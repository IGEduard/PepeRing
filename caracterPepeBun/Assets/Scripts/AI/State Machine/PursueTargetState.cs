using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PursueTargetState : State
{
    public CombatStanceState combatStanceState;
    public RotateTowardsTargetState rotateTowardsTargetState;

    private void Awake() {
        
    }
    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        // chase the target
        // if within attack range, return combat stance state
        // if target is out of range, return this state and continue to chase target

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);
        //float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);

        HandleRotateTowardsTarget(enemyManager);

        /*if (viewableAngle >= 55 || viewableAngle <= -55){
            return rotateTowardsTargetState;
        }*/

        if(enemyManager.isInteracting)
            return this;

        if (enemyManager.isPerformingAction){
            enemyAnimatorManager.animator.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            return this;
        }
        

        if (distanceFromTarget > enemyManager.maximumAggroRange){
            enemyAnimatorManager.animator.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
        }

        if (distanceFromTarget <= enemyManager.maximumAggroRange){
            return combatStanceState;
        }
        else {
            return this;
        }
        
    }

    private void HandleRotateTowardsTarget(EnemyManager enemyManager){
        if (enemyManager.isPerformingAction){ // rotate manually
            Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero){
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
        else { // rotate with pathfinding (navmesh)
            Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyManager.enemyRigidBody.velocity;

            enemyManager.navMeshAgent.enabled = true;
            enemyManager.navMeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
            enemyManager.enemyRigidBody.velocity = targetVelocity;
            enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navMeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
