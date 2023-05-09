using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Cure Effect")]
public class CureConsumableItem : ConsumableItem
{
    [Header("Recovery FX")]
    public GameObject cureConsumeFX;

    [Header("Cure FX")]
    public bool curePoison;
    // cure bleed
    // cure curse

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager,  PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager);

        GameObject cure = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
        playerEffectsManager.currentParticleFX = cureConsumeFX;
        playerEffectsManager.instantiatedFXModel = cure;
        if (curePoison){
            playerEffectsManager.poisonBuildUp = 0;
            playerEffectsManager.poisonAmount = playerEffectsManager.defaultPoisonAmount;
            playerEffectsManager.isPoisoned = false;

            if (playerEffectsManager.currentPoisonParticleFX != null){
                Destroy(playerEffectsManager.currentPoisonParticleFX);
            }
        }
        
        weaponSlotManager.rightHandSlot.UnloadWeapon();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
