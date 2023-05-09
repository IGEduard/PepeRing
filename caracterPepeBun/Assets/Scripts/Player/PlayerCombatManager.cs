using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    InputHandler inputHandler;
    CameraHandler cameraHandler;
    PlayerManager playerManager;
    PlayerAnimatorManager playerAnimatorManager;
    PlayerEquipmentManager playerEquipmentManager;
    PlayerStatsManager playerStatsManager;
    PlayerInventoryManager playerInventoryManager;
    PlayerWeaponSlotManager playerWeaponSlotManager;
    PlayerEffectsManager playerEffectsManager;

    [Header("Attack Animations")]
    private string oH_Light_Attack_01 = "OH_Light_Attack_01";
    private string oH_Light_Attack_02 = "OH_Light_Attack_02";
    private string oH_Heavy_Attack_01 = "OH_Heavy_Attack_01";
    private string oH_Heavy_Attack_02 = "OH_Heavy_Attack_02";

    private string tH_Light_Attack_01 = "TH_Light_Attack_01";
    private string tH_Light_Attack_02 = "TH_Light_Attack_02";
    private string tH_Heavy_Attack_01 = "TH_Heavy_Attack_01";
    private string tH_Heavy_Attack_02 = "TH_Heavy_Attack_02";

    private string weaponArt = "Weapon_Art";

    public string lastAttack;

    LayerMask backStabLayer = 1 << 12;
    LayerMask riposteLayer = 1 << 13;

    private void Awake()
    {
        inputHandler = GetComponent<InputHandler>();
        cameraHandler = FindObjectOfType<CameraHandler>();
        playerManager = GetComponentInParent<PlayerManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerWeaponSlotManager = GetComponent<PlayerWeaponSlotManager>();
        playerEffectsManager = GetComponent<PlayerEffectsManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
    }
    public void HandleLightAttack(WeaponItem weapon)
    {
        if (playerStatsManager.currentStamina <= 0){
            return;
        }
        playerWeaponSlotManager.attackingWeapon = weapon;
        if (inputHandler.twoHandFlag)
        {
            playerAnimatorManager.PlayTargetAnimation(tH_Light_Attack_01, true);
            lastAttack = tH_Light_Attack_01;
        }
        else
        {
            playerAnimatorManager.PlayTargetAnimation(oH_Light_Attack_01, true);
            lastAttack = oH_Light_Attack_01;
        }

    }

    public void HandleStrongAttack(WeaponItem weapon)
    {
        if (playerStatsManager.currentStamina <= 0){
            return;
        }
        playerWeaponSlotManager.attackingWeapon = weapon;
        if (inputHandler.twoHandFlag)
        {
            // to do
            playerAnimatorManager.PlayTargetAnimation(tH_Heavy_Attack_01, true);
            lastAttack = tH_Heavy_Attack_01;
        }
        else
        {
            playerAnimatorManager.PlayTargetAnimation(oH_Heavy_Attack_01, true);
            lastAttack = oH_Heavy_Attack_01;
        }

    }
    public void HandleWeaponCombo(WeaponItem weapon)
    {
        if (playerStatsManager.currentStamina <= 0){
            return;
        }
        if (inputHandler.comboFlag)
        {
            playerAnimatorManager.animator.SetBool("canDoCombo", false);

            if (inputHandler.twoHandFlag)
            {
                if (lastAttack == tH_Light_Attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(tH_Light_Attack_02, true);
                }
                else if (lastAttack == tH_Heavy_Attack_01){
                    playerAnimatorManager.PlayTargetAnimation(tH_Heavy_Attack_02, true);
                }
            }
            else {
                if (lastAttack == oH_Light_Attack_01)
                {
                    playerAnimatorManager.PlayTargetAnimation(oH_Light_Attack_02, true);
                }
                else if (lastAttack == oH_Heavy_Attack_01){
                    playerAnimatorManager.PlayTargetAnimation(oH_Heavy_Attack_02, true);
                }
            }
        }

    }

    #region Input Actions
    public void HandleLightAttackAction()
    {
        playerAnimatorManager.EraseHandIKForWeapon();
        if(playerInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword ||
            playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
        {
            //handle melee action
            PerformLightAttackMeleeAction();
        }
        else if (playerInventoryManager.rightWeapon.weaponType == WeaponType.SpellCaster || 
            playerInventoryManager.rightWeapon.weaponType == WeaponType.FaithCaster || 
            playerInventoryManager.rightWeapon.weaponType == WeaponType.PyromancyCaster)
        {
            // handle magic action
            PerformLightAttackMagicAction(playerInventoryManager.rightWeapon);
        }
    }

    public void HandleStrongAttackAction(){
        if(playerInventoryManager.rightWeapon.weaponType == WeaponType.StraightSword)
        {
            //handle melee action
            PerformStrongAttackMeleeAction();
        }
        
    }

    public void HandleBlockAction(){
        PerformBlockingAction();
    }

    public void HandleParryAction(){
        if(playerInventoryManager.leftWeapon.weaponType == WeaponType.Shield ||
            playerInventoryManager.rightWeapon.weaponType == WeaponType.Unarmed)
        {
            PerformParryWeaponArt(inputHandler.twoHandFlag);
        } else if (playerInventoryManager.leftWeapon.weaponType == WeaponType.StraightSword) {
            // do a light attack
        }
    }

    #endregion

    #region Attack Actions
    private void PerformLightAttackMeleeAction()
    {
        
        if (playerManager.canDoCombo)
        {
            inputHandler.comboFlag = true;
            HandleWeaponCombo(playerInventoryManager.rightWeapon);
            inputHandler.comboFlag = false;
        }
        else
        {
            if (playerManager.isInteracting || playerManager.canDoCombo || playerManager.isInAir) 
            {
                return;
            }

            playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
            HandleLightAttack(playerInventoryManager.rightWeapon);
        }

        playerEffectsManager.PlayWeaponFX(false);
    }

    private void PerformStrongAttackMeleeAction()
    {
        if (playerManager.canDoCombo)
        {
            inputHandler.comboFlag = true;
            HandleWeaponCombo(playerInventoryManager.rightWeapon);
            inputHandler.comboFlag = false;
        }
        else
        {
            if (playerManager.isInteracting || playerManager.canDoCombo || playerManager.isInAir) 
            {
                return;
            }

            playerAnimatorManager.animator.SetBool("isUsingRightHand", true);
            HandleStrongAttack(playerInventoryManager.rightWeapon);
        }
    }

    private void PerformLightAttackMagicAction(WeaponItem weapon)
    {
        if(weapon.weaponType == WeaponType.FaithCaster)
        {
            if(playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isFaithSpell){
                // check for fp
                // attemp to cast spell
                if(playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost){
                    playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager);
                }
                else{
                    playerAnimatorManager.PlayTargetAnimation("Shrug", true);
                }
            }
        }
        else if (weapon.weaponType == WeaponType.PyromancyCaster)
        {
            if(playerInventoryManager.currentSpell != null && playerInventoryManager.currentSpell.isPyroSpell){
                // check for fp
                // attemp to cast spell
                if(playerStatsManager.currentFocusPoints >= playerInventoryManager.currentSpell.focusPointCost){
                    playerInventoryManager.currentSpell.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager);
                }
                else{
                    playerAnimatorManager.PlayTargetAnimation("Shrug", true);
                }
            }
        }
    }

    private void PerformParryWeaponArt(bool isTwoHanding){
        if(playerManager.isInteracting)
            return;
        if(isTwoHanding) {
            // if two hand perform weapon art
        } else {
            playerAnimatorManager.PlayTargetAnimation(weaponArt, true);
        }
        
    }

    private void SuccessfullyCastSpell(){
        playerInventoryManager.currentSpell.SuccessfullyCastSpell(playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager);
        playerAnimatorManager.animator.SetBool("isFiringSpell", true);
    }

    #endregion

    #region Defense Actions
    private void PerformBlockingAction(){
        if (playerManager.isInteracting){
            return;
        }
        if(playerManager.isBlocking)
            return;
        playerAnimatorManager.PlayTargetAnimation("BlockStart", false, true);
        playerEquipmentManager.OpenBlockingCollider();
        playerManager.isBlocking = true;
    }
    #endregion
    
    public void AttemptBackStabOrRiposte(){
        if (playerStatsManager.currentStamina <= 0){
            return;
        }
        
        RaycastHit hit;

        if(Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

            if (enemyCharacterManager != null)
            {
                // check for team ID ( so you cant backstab yourself or friend)
                // pull is into a transform behind the enemy so the backstab looks clean
                // play animation
                // make enemy play animation
                // do damage
                playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;
                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayTargetAnimation("Back Stab", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Back Stabbed", true);
            }
        }
        else if(Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer))
        {
            CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
            DamageCollider rightWeapon = playerWeaponSlotManager.rightHandDamageCollider;

            if(enemyCharacterManager != null && enemyCharacterManager.canBeRiposted)
            {
                playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                rotationDirection = hit.transform.position - playerManager.transform.position;
                rotationDirection.y = 0;
                rotationDirection.Normalize();
                Quaternion tr = Quaternion.LookRotation(rotationDirection);
                Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                playerManager.transform.rotation = targetRotation;

                int criticalDamage = playerInventoryManager.rightWeapon.criticalDamageMultiplier * rightWeapon.physicalDamage;
                enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                playerAnimatorManager.PlayTargetAnimation("Riposte", true);
                enemyCharacterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Riposted", true);
            }
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
