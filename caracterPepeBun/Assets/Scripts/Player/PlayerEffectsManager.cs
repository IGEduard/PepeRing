using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectsManager : CharacterEffectsManager
{
    PlayerStatsManager playerStatsManager;
    PlayerWeaponSlotManager playerWeaponSlotManager;

    PoisonBuildUpBar poisonBuildUpBar;
    PoisonAmountBar poisonAmountBar;
    
    public GameObject currentParticleFX; // the particles that will play of the current effect that is affecting the player (drinking estus)
    public GameObject instantiatedFXModel;
    
    public int amountToBeHealed;

    protected override void Awake() {
        base.Awake();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();

        poisonBuildUpBar = FindObjectOfType<PoisonBuildUpBar>();
        poisonAmountBar = FindObjectOfType<PoisonAmountBar>();
    }

    public void HealPlayerFromEffect(){
        playerStatsManager.HealPlayer(amountToBeHealed);
        GameObject healParticles = Instantiate(currentParticleFX, playerStatsManager.transform);
        Destroy(instantiatedFXModel.gameObject);
        playerWeaponSlotManager.LoadBothWeaponsOnSlots();
    }

    protected override void HandlePoisonBuildUp(){
        if(poisonBuildUp <= 0){
            poisonBuildUpBar.gameObject.SetActive(false);
        }
        else {
            poisonBuildUpBar.gameObject.SetActive(true);
        }
        base.HandlePoisonBuildUp();
        poisonBuildUpBar.SetPoisonBuildUpAmount(Mathf.RoundToInt(poisonBuildUp));
    }

    protected override void HandleIsPoisonedEffect(){
        if (isPoisoned == false){
            poisonAmountBar.gameObject.SetActive(false);
        }
        else {
            poisonAmountBar.gameObject.SetActive(true);
        }
        base.HandleIsPoisonedEffect();
        poisonAmountBar.SetPoisonAmount(Mathf.RoundToInt(poisonAmount));
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
