using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    CharacterStatsManager characterStatsManager;

    [Header("Damage FX")]
    public GameObject bloodSplatterFX;

    [Header("Weapon FX")]
    public WeaponFX rightWeaponFX;
    public WeaponFX leftWeaponFX;

    [Header("Poison")]
    public GameObject defaultPoisonParticleFX;
    public GameObject currentPoisonParticleFX;
    public Transform buildUpTransform; // the location build up particle FX will spawn
    public bool isPoisoned;
    public float poisonBuildUp = 0; // the build up over time that poisons the player after reaching 100
    public float poisonAmount = 100; // the amount of poison the player has to process before becoming unpoisoned
    public float defaultPoisonAmount = 100; // the dafault amount of poison a player has to process once they become poisoned
    public float poisonTimer = 2;   // the amount of time between each poison damage Tick
    public int poisonDamage = 1;
    float timer;

    protected virtual void Awake(){
        characterStatsManager = GetComponent<CharacterStatsManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public virtual void PlayWeaponFX(bool isLeft){
        if (isLeft == false) {
            // play right weapons trail
            if (rightWeaponFX != null)
            {
                rightWeaponFX.PlayWeaponFX();
            }
        } else {
            // play left weapons trail
            if (leftWeaponFX != null)
            {
                leftWeaponFX.PlayWeaponFX();
            }
        }
    }

    public virtual void PlayBloodSplatterFX(Vector3 bloodSplatterLocation){
        GameObject blood = Instantiate(bloodSplatterFX, bloodSplatterLocation, Quaternion.identity);
    }

    public virtual void HandleAllBuildUpEffects(){
        if (characterStatsManager.isDead){
            return;
        }

        HandlePoisonBuildUp();
        HandleIsPoisonedEffect();
    }

    protected virtual void HandlePoisonBuildUp(){
        if(isPoisoned){
            return;
        }
        if (characterStatsManager.isDead){
            return;
        }
        if (poisonBuildUp > 0 && poisonBuildUp < 100){
            poisonBuildUp = poisonBuildUp - 1 * Time.deltaTime;
        } 
        else if (poisonBuildUp >= 100){
            isPoisoned = true;
            poisonBuildUp = 0;

            if (buildUpTransform != null){
                currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, buildUpTransform.transform);
            }
            else {
                currentPoisonParticleFX = Instantiate(defaultPoisonParticleFX, characterStatsManager.transform);
            }
        }
    }

    protected virtual void HandleIsPoisonedEffect(){
        if (isPoisoned){
            
            if (poisonAmount > 0){
                timer += Time.deltaTime;

                if(timer >= poisonTimer){
                    characterStatsManager.TakePoisonDamage(poisonDamage);
                    timer = 0;
                }
                
                poisonAmount = poisonAmount - 1 * Time.deltaTime;
            }
            else{
                isPoisoned = false;
                poisonAmount = defaultPoisonAmount;
                Destroy(currentPoisonParticleFX);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
