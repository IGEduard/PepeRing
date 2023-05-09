using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsTargetState : State
{
    public CombatStanceState combatStanceState;

    public override State Tick(EnemyManager enemyManager, EnemyStatsManager enemyStats, EnemyAnimatorManager enemyAnimatorManager)
    {
        enemyAnimatorManager.animator.SetFloat("Vertical", 0);
        enemyAnimatorManager.animator.SetFloat("Horizontal", 0);

        Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
        float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

        if (enemyManager.isInteracting){
            return this; // when we enter the state we will still be interacting from the attack animation so we pause here until it has finished
        }
        if (viewableAngle >= 100 && viewableAngle <= 180 && !enemyManager.isInteracting){
            enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Behind", true);
            return combatStanceState;
        }
        else if (viewableAngle <= -101 && viewableAngle >= -180 && !enemyManager.isInteracting){
            enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Behind", true);
            return combatStanceState;
        } 
        else if (viewableAngle <= -45 && viewableAngle >= -100 && !enemyManager.isInteracting){
            enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Right", true);
            return combatStanceState;
        }
        else if (viewableAngle >= 45 && viewableAngle <= 100 && !enemyManager.isInteracting){
            enemyAnimatorManager.PlayTargetAnimationWithRootRotation("Turn_Left", true);
            return combatStanceState;
        }

        return combatStanceState;
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
