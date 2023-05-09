using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    public PlayerManager playerManager;
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public FocusPointBar focusPointBar;

    PlayerAnimatorManager playerAnimatorManager;

    public float staminaRegenerationAmount = 25;
    public float staminaRegenerationTimer = 0;

    protected override void Awake() {
        base.Awake();
        //healthBar = FindObjectOfType<HealthBar>();
        playerManager = GetComponent<PlayerManager>();
        staminaBar = FindObjectOfType<StaminaBar>();
        focusPointBar = FindObjectOfType<FocusPointBar>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = SetMaxHealthFromHealthLevel();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetCurrentHealth(currentHealth);

        maxStamina = SetMaxStaminaFromStaminaLevel();
        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);
        staminaBar.SetCurrentStamina(currentStamina);

        maxFocusPoints = SetMaxFocusPointsFromFocusLevel();
        currentFocusPoints = maxFocusPoints;
        focusPointBar.SetMaxFocusPoints(maxFocusPoints);
        focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
    }

    public override void HandlePoiseResetTimer()
    {
        if (poiseResetTimer > 0){
            poiseResetTimer = poiseResetTimer - Time.deltaTime;
        }
        else if (poiseResetTimer <= 0 && !playerManager.isInteracting){
            totalPoiseDefence = armorPoiseBonus;
        }
    }

    private int SetMaxHealthFromHealthLevel(){
        maxHealth = healthLevel * 10;
        return maxHealth;
    }

    private float SetMaxStaminaFromStaminaLevel(){
        maxStamina = staminaLevel * 10;
        return maxStamina;
    }

    private float SetMaxFocusPointsFromFocusLevel(){
        maxFocusPoints = focusLevel * 10;
        return maxFocusPoints;
    }

    public override void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "TakeDamage"){
        if(playerManager.isInvulnerable){
            return;
        }

        base.TakeDamage(physicalDamage, fireDamage, damageAnimation = "TakeDamage");
        healthBar.SetCurrentHealth(currentHealth);
        playerAnimatorManager.PlayTargetAnimation(damageAnimation, true);

        if(currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
            playerAnimatorManager.PlayTargetAnimation("Dying", true);
        }

    }

    public override void TakeDamageNoAnimation(int physicalDamage, int fireDamage){
        base.TakeDamageNoAnimation(physicalDamage, fireDamage);
        healthBar.SetCurrentHealth(currentHealth);
    }

    public override void TakePoisonDamage(int damage)
    {
        if (isDead){
            return;
        }
        
        base.TakePoisonDamage(damage);
        healthBar.SetCurrentHealth(currentHealth);

        if(currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
            playerAnimatorManager.PlayTargetAnimation("Dying", true);
        }
    }

    public void TakeStaminaDamage(int damage){
        currentStamina = currentStamina - damage;
        staminaBar.SetCurrentStamina(currentStamina);
    }

    public void RegenerateStamina(){
        if(playerManager.isInteracting)
        {
            staminaRegenerationTimer = 0;
        }
        else
        {
            staminaRegenerationTimer += Time.deltaTime;
            
            if(currentStamina < maxStamina && staminaRegenerationTimer > 1f)
            {
                currentStamina += staminaRegenerationAmount * Time.deltaTime;
                staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
            }
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        healthBar.SetCurrentHealth(currentHealth);
    }

    public void DeductFocusPoints(int focusPoints){
        currentFocusPoints -= focusPoints;

        if (currentFocusPoints < 0){
            currentFocusPoints = 0;
        }
        focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
    }

    public void AddSouls(int souls){
        soulCount += souls;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
