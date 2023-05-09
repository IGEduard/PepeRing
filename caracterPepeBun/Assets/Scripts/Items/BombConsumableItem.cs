using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Consumables/Bomb Item")]
public class BombConsumableItem : ConsumableItem
{
    [Header("Velocity")]
    public int upwardVelocity = 50;
    public int forwardVelocity = 50;
    public int bombMass = 1;

    [Header("Live Bomb Model")]
    public GameObject liveBombModel;

    [Header("Base Damage")]
    public int baseDamage = 50;
    public int explosiveDamage = 25;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager){
        if (currentItemAmount > 0){
            weaponSlotManager.rightHandSlot.UnloadWeapon();
            playerAnimatorManager.PlayTargetAnimation(consumeAnimation, true);
            GameObject bombModel = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform.position, Quaternion.identity, weaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.instantiatedFXModel = bombModel;
        } 
        else {
            playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
