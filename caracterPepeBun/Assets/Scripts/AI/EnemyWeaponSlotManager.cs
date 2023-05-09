using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSlotManager : CharacterWeaponSlotManager
{
    protected override void Awake() {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBothWeaponsOnSlots();
        attackingWeapon = rightHandSlot.currentWeapon;
    }

    protected override void GrantWeaponAttackingPoiseBonus(){
        characterStatsManager.totalPoiseDefence = characterStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
        //playerStatsManager.totalPoiseDefence = playerStatsManager.totalPoiseDefence + attackingWeapon.offensivePoiseBonus;
    }

    protected override void ResetWeaponAttackingPoiseBonus(){
        characterStatsManager.totalPoiseDefence = characterStatsManager.armorPoiseBonus;
        // playerStatsManager
    }

    public void DrainStaminaLightAttack()
    {

    }
    public void DrainStaminaHeavyAttack()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
