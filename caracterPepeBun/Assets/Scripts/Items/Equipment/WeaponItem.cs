using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon Item")]
public class WeaponItem : Item
{
    public GameObject modelPrefab;
    public bool isUnarmed;

    [Header("Animator Replacer")]
    public AnimatorOverrideController weaponController;
    public string offHandIdleAnimation = "Left_Hand_Idle_01";

    [Header("Weapon Type")]
    public WeaponType weaponType;

    [Header("Damage")]
    public int physicalDamage;
    public int fireDamage;
    public int criticalDamageMultiplier = 3;

    [Header("Poise")]
    public float poiseBreak;
    public float offensivePoiseBonus;

    [Header("Absorption")]
    public float physicalDamageAbsorption;
    public float fireDamageAbsorption;

/*
    [Header("Idle Animations")]
    public string left_hand_idle;
    public string right_hand_idle;
    public string two_hand_idle;

    [Header("Attack Animations")]
    public string OH_Light_Attack_1;
    public string OH_Light_Attack_2;

    public string OH_Heavy_Attack_1;

    public string TH_Light_Attack_1;
    public string TH_Light_Attack_2;

    [Header("Weapon Art")]
    public string weaponArt;
*/
    [Header("Stamina Costs")]
    public int baseStamina;
    public float lightAttackMultiplier;
    public float heavyAttackMultiplier;
/*
    [Header("Weapon Type")]
    public bool isSpellCaster;
    public bool isFaithCaster;
    public bool isPyroCaster;
    public bool isMeleeWeapon;
    public bool isShieldWeapon;
*/
}
