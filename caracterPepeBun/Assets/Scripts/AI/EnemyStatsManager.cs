using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : CharacterStatsManager
{
    //public HealthBar healthBar;
    EnemyBossManager enemyBossManager;
    EnemyAnimatorManager enemyAnimatorManager;
    WorldEventManager worldEventManager; // new
    public UIEnemyHealthBar enemyHealthBar;
    
    public bool isBoss;

    protected override void Awake() {
        base.Awake();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        enemyBossManager = GetComponent<EnemyBossManager>();
        maxHealth = SetMaxHealthFromHealthLevel();
        worldEventManager = FindObjectOfType<WorldEventManager>(); // new
        currentHealth = maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        //healthBar.SetMaxHealth(maxHealth);
        if (!isBoss){
            enemyHealthBar.SetMaxHealth(maxHealth);
        }
    }

    public override void HandlePoiseResetTimer()
    {
        base.HandlePoiseResetTimer();
    }

    private int SetMaxHealthFromHealthLevel(){
        maxHealth = healthLevel *10;
        return maxHealth;
    }

    public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage){
        base.TakeDamageNoAnimation(physicalDamage, fireDamage);
        
        if (!isBoss){
            enemyHealthBar.SetHealth(currentHealth);
        } else if (isBoss && enemyBossManager != null) {
            enemyBossManager.SetBossHealthBar(currentHealth, maxHealth);

        }
        //animator.Play("Take Damage");

        if(currentHealth <= 0){
            HandleDeath();

        }
    }

    public override void BreakGuard(){
        enemyAnimatorManager.PlayTargetAnimation("Break_Guard", true);
    }

    public override void TakePoisonDamage(int damage)
    {
        if (isDead){
            return;
        }
        
        base.TakePoisonDamage(damage);
        
        if (!isBoss){
            enemyHealthBar.SetHealth(currentHealth);
        } else if (isBoss && enemyBossManager != null) {
            enemyBossManager.SetBossHealthBar(currentHealth, maxHealth);
        }

        if(currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
            enemyAnimatorManager.PlayTargetAnimation("Dying", true);
        }
    }

    public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "TakeDamage"){

        base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "TakeDamage");
        

        if (!isBoss){
            enemyHealthBar.SetHealth(currentHealth);
        } else if (isBoss && enemyBossManager != null) {
            enemyBossManager.SetBossHealthBar(currentHealth, maxHealth);
        }
        
        enemyAnimatorManager.PlayTargetAnimation(damageAnimation, true);
        //animator.Play("Take Damage");

        if(currentHealth <= 0){
            HandleDeath();
        }
    }


    private void HandleDeath(){
        if (isBoss && enemyBossManager != null) {
            worldEventManager.BossHasBeenDefeated();
        }
        currentHealth = 0;
        enemyAnimatorManager.PlayTargetAnimation("Dying", true);
        //animator.Play("Dying");
        isDead = true;


        //scan for every player in the scene
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
