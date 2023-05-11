using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    [Header("Team ID")]
    public int teamIDNumber = 0;
    CharacterAnimatorManager characterAnimatorManager;
    public int healthLevel = 10;
    public int maxHealth;
    public int currentHealth;

    public int staminaLevel = 10;
    public float maxStamina;
    public float currentStamina;

    public int focusLevel = 10;
    public float maxFocusPoints;
    public float currentFocusPoints;

    public int soulCount = 0;
    public int soulsAwardedOnDeath = 50;

    [Header("Poise")]
    public float totalPoiseDefence;
    public float offensivePoiseBonus; // the poise you gain from a weapon
    public float armorPoiseBonus; // the poise you gain from armor
    public float totalPoiseResetTime = 5;
    public float poiseResetTimer = 0;

    [Header("Armor Absorptions")]
    public float physicalDamageAbsorptionHead;
    public float physicalDamageAbsorptionTorso;
    public float physicalDamageAbsorptionLegs;
    public float physicalDamageAbsorptionHands;

    public float fireDamageAbsorptionHead;
    public float fireDamageAbsorptionTorso;
    public float fireDamageAbsorptionLegs;
    public float fireDamageAbsorptionHands;

    // fire absorption, lighting, magic, dark

    public bool isDead;

    protected virtual void Awake(){
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
    }

    public virtual void TakeDamage(int physicalDamage, int fireDamage, string damageAnimation = "TakeDamage"){
        if (isDead){
            return;
        }


        //characterAnimatorManager.EraseHandIKForWeapon(); // maybe solve this error for new boss

        /*float totalPhysicalDamageAbsorption = 1 - 
            (1 - physicalDamageAbsorptionHead / 100) *
            (1 - physicalDamageAbsorptionTorso / 100) *
            (1 - physicalDamageAbsorptionLegs / 100) *
            (1 - physicalDamageAbsorptionHands / 100);*/

        float totalPhysicalDamageAbsorption = 
            ( physicalDamageAbsorptionHead / 100) +
            ( physicalDamageAbsorptionTorso / 100) +
            ( physicalDamageAbsorptionLegs / 100) +
            ( physicalDamageAbsorptionHands / 100);

        Debug.Log("Total phy dmg absorption: " + totalPhysicalDamageAbsorption + "%");

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

        float totalFireDamageAbsorption = 
            ( fireDamageAbsorptionHead / 100) +
            ( fireDamageAbsorptionTorso / 100) +
            ( fireDamageAbsorptionLegs / 100) +
            ( fireDamageAbsorptionHands / 100);

        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

        float finalDamage = physicalDamage + fireDamage; // + magicDamage + lightingDamage + darkDamage

        Debug.Log("Total Damage Dealt: " + finalDamage);

        currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

        if (currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void TakeDamageNoAnimation(int physicalDamage, int fireDamage){
        if (isDead){
            return;
        }

        characterAnimatorManager.EraseHandIKForWeapon();

        /*float totalPhysicalDamageAbsorption = 1 - 
            (1 - physicalDamageAbsorptionHead / 100) *
            (1 - physicalDamageAbsorptionTorso / 100) *
            (1 - physicalDamageAbsorptionLegs / 100) *
            (1 - physicalDamageAbsorptionHands / 100);*/

        float totalPhysicalDamageAbsorption = 
            ( physicalDamageAbsorptionHead / 100) +
            ( physicalDamageAbsorptionTorso / 100) +
            ( physicalDamageAbsorptionLegs / 100) +
            ( physicalDamageAbsorptionHands / 100);

        Debug.Log("Total phy dmg absorption: " + totalPhysicalDamageAbsorption + "%");

        physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

        float totalFireDamageAbsorption = 
            ( fireDamageAbsorptionHead / 100) +
            ( fireDamageAbsorptionTorso / 100) +
            ( fireDamageAbsorptionLegs / 100) +
            ( fireDamageAbsorptionHands / 100);

        fireDamage = Mathf.RoundToInt(fireDamage - (fireDamage * totalFireDamageAbsorption));

        float finalDamage = physicalDamage + fireDamage; // + magicDamage + lightingDamage + darkDamage

        Debug.Log("Total Damage Dealt: " + finalDamage);

        currentHealth = Mathf.RoundToInt(currentHealth - finalDamage);

        
        if(currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
        }
    }

    public virtual void BreakGuard(){
        characterAnimatorManager.PlayTargetAnimation("Break_Guard", true);
    }

    public virtual void HandlePoiseResetTimer(){
        if (poiseResetTimer > 0){
            poiseResetTimer = poiseResetTimer - Time.deltaTime;
        }
        else {
            totalPoiseDefence = armorPoiseBonus;
        }
    }

    public virtual void TakePoisonDamage(int damage){
        currentHealth = currentHealth - damage;
        
        if(currentHealth <= 0){
            currentHealth = 0;
            isDead = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalPoiseDefence = armorPoiseBonus;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HandlePoiseResetTimer();
    }
}
