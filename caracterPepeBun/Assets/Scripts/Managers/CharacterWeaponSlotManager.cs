using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeaponSlotManager : MonoBehaviour
{
    protected CharacterManager characterManager;
    protected CharacterStatsManager characterStatsManager;
    protected CharacterEffectsManager characterEffectsManager;
    protected CharacterInventoryManager characterInventoryManager;
    protected CharacterAnimatorManager characterAnimatorManager;
    
    [Header("Unarmed Weapon")]
    public WeaponItem unarmedWeapon;

    [Header("Weapon Slots")]
    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;
    public WeaponHolderSlot backSlot;

    [Header("Damage Colliders")]
    public DamageCollider leftHandDamageCollider;
    public DamageCollider rightHandDamageCollider;

    [Header("Attacking Weapon")]
    public WeaponItem attackingWeapon;

    [Header("Hand IK Targets")]
    public RightHandIKTarget rightHandIKTarget;
    public LeftHandIKTarget leftHandIKTarget;

    protected virtual void Awake() {
        
        characterManager = GetComponent<CharacterManager>();
        characterStatsManager = GetComponent<CharacterStatsManager>();
        characterEffectsManager = GetComponent<CharacterEffectsManager>();
        characterInventoryManager = GetComponent<CharacterInventoryManager>();
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        LoadWeaponHolderSlots();
    }

    // Start is called before the first frame update
    void Start()
    {
        //LoadBothWeaponsOnSlots();
    }

    protected virtual void LoadWeaponHolderSlots(){
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots)
        {
            if (weaponSlot.isLeftHandSlot)
            {
                leftHandSlot = weaponSlot;
            }
            else if (weaponSlot.isRightHandSlot)
            {
                rightHandSlot = weaponSlot;
            }
            else if (weaponSlot.isBackSlot)
            {
                backSlot = weaponSlot;
            }
        }
    }

    public virtual void LoadBothWeaponsOnSlots(){
        LoadWeaponOnSlot(characterInventoryManager.rightWeapon, false);
        LoadWeaponOnSlot(characterInventoryManager.leftWeapon, true);
    }

    public virtual void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft)
    {
        if (weaponItem != null )
        {
            if (isLeft)
            {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                //characterAnimatorManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            }
            else
            {
                if (characterManager.isTwoHandingWeapon)
                {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeapon();
                    characterAnimatorManager.PlayTargetAnimation("Left Hand Empty", false, true);
                }
                else
                {
                    backSlot.UnloadWeaponAndDestroy();
                }
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                LoadTwoHandIKTargets(characterManager.isTwoHandingWeapon);
                characterAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        } 
        else 
        {
            weaponItem = unarmedWeapon;
            if (isLeft){
                characterInventoryManager.leftWeapon = weaponItem;
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                characterAnimatorManager.PlayTargetAnimation(weaponItem.offHandIdleAnimation, false, true);
            } else {
                characterInventoryManager.rightWeapon = weaponItem;
                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                characterAnimatorManager.animator.runtimeAnimatorController = weaponItem.weaponController;
            }
        }
    }

    protected virtual void GrantWeaponAttackingPoiseBonus(){
        characterStatsManager.totalPoiseDefence = characterStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
        //playerStatsManager.totalPoiseDefence = playerStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
    }

    protected virtual void ResetWeaponAttackingPoiseBonus(){
        characterStatsManager.totalPoiseDefence = characterStatsManager.armorPoiseBonus;
        // playerStatsManager
    }

    protected virtual void LoadLeftWeaponDamageCollider()
    {
        //leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        leftHandDamageCollider.physicalDamage = characterInventoryManager.leftWeapon.physicalDamage;
        leftHandDamageCollider.fireDamage = characterInventoryManager.leftWeapon.fireDamage;

        leftHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

        leftHandDamageCollider.poiseBreak = characterInventoryManager.leftWeapon.poiseBreak;

        characterEffectsManager.leftWeaponFX = leftHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
    }
    protected virtual void LoadRightWeaponDamageCollider()
    {
        //rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>(); 
        rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();

        rightHandDamageCollider.physicalDamage = characterInventoryManager.rightWeapon.physicalDamage;
        rightHandDamageCollider.fireDamage = characterInventoryManager.rightWeapon.fireDamage;

        rightHandDamageCollider.teamIDNumber = characterStatsManager.teamIDNumber;

        rightHandDamageCollider.poiseBreak = characterInventoryManager.rightWeapon.poiseBreak;
        
        characterEffectsManager.rightWeaponFX = rightHandSlot.currentWeaponModel.GetComponentInChildren<WeaponFX>();
    }

    public virtual void LoadTwoHandIKTargets(bool isTwoHandingWeapon){
        leftHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<LeftHandIKTarget>();
        rightHandIKTarget = rightHandSlot.currentWeaponModel.GetComponentInChildren<RightHandIKTarget>();

        characterAnimatorManager.SetHandIKForWeapon(rightHandIKTarget, leftHandIKTarget, isTwoHandingWeapon);
    }

    public virtual void OpenDamageCollider()
    {
        if(characterManager.isUsingRightHand){
            rightHandDamageCollider.EnableDamageCollider();
        }
        else if (characterManager.isUsingLeftHand){
            leftHandDamageCollider.EnableDamageCollider();
        }
        
    }

    public virtual void CloseDamageCollider()
    {
        if (rightHandDamageCollider != null){
            rightHandDamageCollider.DisableDamageCollider();
        }
        if (leftHandDamageCollider != null){
            leftHandDamageCollider.DisableDamageCollider();
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}