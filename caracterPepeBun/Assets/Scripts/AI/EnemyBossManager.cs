using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossManager : MonoBehaviour
{
    public string bossName;
    UIBossHealthBar bossHealthBar;
    EnemyStatsManager enemyStats;
    EnemyAnimatorManager enemyAnimatorManager;
    BossCombatStanceState bossCombatStanceState;

    [Header("Second Phase FX")]
    public GameObject particleFX;

    private void Awake() {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>(); // one boss per scene
        enemyStats = GetComponent<EnemyStatsManager>();
        enemyAnimatorManager = GetComponentInChildren<EnemyAnimatorManager>();
        bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bossHealthBar.SetBossName(bossName);
        bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
    }

    public void SetBossHealthBar(int currentHealth, int maxHealth){
        bossHealthBar.SetBossCurrentHealth(currentHealth);

        if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted){
            bossCombatStanceState.hasPhaseShifted = true;
            ShiftToSecondPhase();
        }
    }

    public void ShiftToSecondPhase(){
        // play an animation with an event that triggers particle fx/weapon fx
        // switch attack actions
        enemyAnimatorManager.animator.SetBool("isInvulnerable", true);
        enemyAnimatorManager.animator.SetBool("isPhaseShifting", true);
        enemyAnimatorManager.PlayTargetAnimation("Phase Shift", true);
        bossCombatStanceState.hasPhaseShifted = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
