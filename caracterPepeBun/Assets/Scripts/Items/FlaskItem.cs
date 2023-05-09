using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Flask")]

public class FlaskItem : ConsumableItem
{
    [Header("Flask Type")]
    public bool estusFlask;
    public bool ashenFlask;

    [Header("Recovery Amount")]
    public int healthRecoverAmount;
    public int focusPointsRecoverAmount;

    [Header("Recovery FX")]
    public GameObject recoveryFX;

    public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager,  PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager)
    {
        base.AttemptToConsumeItem(playerAnimatorManager, weaponSlotManager, playerEffectsManager);

        // instantiate flask in hand and play drink animation
        GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
        // add health or fp
        playerEffectsManager.currentParticleFX = recoveryFX;
        playerEffectsManager.amountToBeHealed = healthRecoverAmount;
        // play recovery fx when/if we drink without being hit
        playerEffectsManager.instantiatedFXModel = flask;
        
        weaponSlotManager.rightHandSlot.UnloadWeapon();
        
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
