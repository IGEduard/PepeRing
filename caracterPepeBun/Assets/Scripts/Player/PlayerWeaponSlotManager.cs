using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponSlotManager : CharacterWeaponSlotManager
{
    InputHandler inputHandler;
    CameraHandler cameraHandler;
    PlayerManager playerManager;
    PlayerInventoryManager playerInventoryManager;
    PlayerStatsManager playerStatsManager;
    PlayerEffectsManager playerEffectsManager;
    PlayerAnimatorManager playerAnimatorManager;
    QuickSlotsUI quickSlotsUI;

    protected override void Awake()
    {   
        base.Awake();
        cameraHandler = FindObjectOfType<CameraHandler>();
        inputHandler = GetComponent<InputHandler>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerManager = GetComponent<PlayerManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
        
    }

    public override void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (weaponItem != null )
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                //animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                playerAnimatorManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                if (inputHandler.twoHandFlag)
                {
                    //Move current left hand weapon to the back or disable it
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeapon();
                    playerAnimatorManager.PlayTargetAnimation("Left Hand Empty", false, true);
                    //animator.CrossFade(weaponItem.two_hand_idle, 0.2f);
                }
                else
                {
                    //animator.CrossFade("Two Hand Empty", 0.2f);
                    backSlot.UnloadWeaponAndDestroy();
                    //animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                }
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                playerAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        } 
        else 
        {
            weaponItem = unarmedWeapon;
            if (isLeft){
                //animator.CrossFade("Left Hand Empty", 0.2f);
                playerInventoryManager.leftWeapon = weaponItem;
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);
                //playerAnimatorManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            } else {
                //animator.CrossFade("Right Hand Empty", 0.2f);
                playerInventoryManager.rightWeapon = weaponItem;
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
                playerAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        }
    }

    public void SuccessfullyThrowFireBomb(){
        Destroy(playerEffectsManager.instantiatedFXModel);
        BombConsumableItem fireBombItem = playerInventoryManager.currentConsumable as BombConsumableItem;

        GameObject activeModelBomb = Instantiate(fireBombItem.liveBombModel, rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
        activeModelBomb.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerManager.lockOnTransform.eulerAngles.y, 0);
        // detect bomb damage collider
        BombDamageCollider damageCollider = activeModelBomb.GetComponentInChildren<BombDamageCollider>();
        // add force to rigidbody to move it through the air
        damageCollider.explosionDamage = fireBombItem.baseDamage;
        damageCollider.explosionSplashDamage = fireBombItem.explosiveDamage;
        damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.forward * fireBombItem.forwardVelocity);
        damageCollider.bombRigidBody.AddForce(activeModelBomb.transform.up * fireBombItem.upwardVelocity);
        
        damageCollider.teamIDNumber = playerStatsManager.teamIDNumber;

        LoadWeaponOnSlot(playerInventoryManager.rightWeapon, false);
        // check for friendly fire
    }

    public void DrainStaminaLightAttack()
    {
        playerStatsManager.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
    }
    public void DrainStaminaHeavyAttack()
    {
        playerStatsManager.TakeStaminaDamage(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBothWeaponsOnSlots();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
