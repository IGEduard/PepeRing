using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItem : Item
{
    [Header("Item Quantity")]
    public int maxItemAmount;
    public int currentItemAmount;

    [Header("Item Model")]
    public GameObject itemModel;

    [Header("Animations")]
    public string consumeAnimation;
    public bool isInteracting;

    public virtual void AttemptToConsumeItem(PlayerAnimatorManager playerAnimatorManager, PlayerWeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager){
        if (currentItemAmount > 0){
            playerAnimatorManager.PlayTargetAnimation(consumeAnimation, isInteracting, true);
        } else {
            playerAnimatorManager.PlayTargetAnimation("Shrug", true);
        }
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
