using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Spells/Projectile Spell")]
public class ProjectileSpell : SpellItem
{
    [Header("Projectile Damage")]
    public float baseDamage;

    [Header("Projectile Physics")]
    public float projectileForwardVelocity;
    public float projectileUpwardVelocty;
    public float projectileMass;
    public bool isAffectedByGravity;
    Rigidbody rigidBody;

    public override void AttemptToCastSpell(PlayerAnimatorManager playerAnimatorManager, PlayerStatsManager playerStatsManager, PlayerWeaponSlotManager playerWeaponSlotManager)
    {
        base.AttemptToCastSpell(playerAnimatorManager, playerStatsManager, playerWeaponSlotManager);
        // instantiate the spell in the casting hand of the player
        GameObject instantiatedWarmUpSpellFx = Instantiate(spellWarmUpFX, playerWeaponSlotManager.rightHandSlot.transform);
        instantiatedWarmUpSpellFx.gameObject.transform.localScale = new Vector3(1,1,1);
        playerAnimatorManager.PlayTargetAnimation(spellAnimation, true);
        // play animation to cast the spell
    }

    public override void SuccessfullyCastSpell(PlayerAnimatorManager playerAnimatorManager, PlayerStatsManager playerStatsManager, CameraHandler cameraHandler, PlayerWeaponSlotManager playerWeaponSlotManager)
    {
        base.SuccessfullyCastSpell(playerAnimatorManager, playerStatsManager, cameraHandler, playerWeaponSlotManager);
        GameObject instantiatedSpellFX = Instantiate(spellCastFX, playerWeaponSlotManager.rightHandSlot.transform.position, cameraHandler.cameraPivotTransform.rotation);
        SpellDamageCollider spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();
        spellDamageCollider.teamIDNumber = playerStatsManager.teamIDNumber;
        
        rigidBody = instantiatedSpellFX.GetComponent<Rigidbody>();
        //spellDamageCollider = instantiatedSpellFX.GetComponent<SpellDamageCollider>();

        if (cameraHandler.currentLockOnTarget != null){
            instantiatedSpellFX.transform.LookAt(cameraHandler.currentLockOnTarget.transform);
        } else {
            instantiatedSpellFX.transform.rotation = Quaternion.Euler(cameraHandler.cameraPivotTransform.eulerAngles.x, playerStatsManager.transform.eulerAngles.y, 0);
        }
        rigidBody.AddForce(instantiatedSpellFX.transform.forward * projectileForwardVelocity);
        rigidBody.AddForce(instantiatedSpellFX.transform.up * projectileUpwardVelocty);
        rigidBody.useGravity = isAffectedByGravity;
        rigidBody.mass = projectileMass;
        instantiatedSpellFX.transform.parent = null;
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
