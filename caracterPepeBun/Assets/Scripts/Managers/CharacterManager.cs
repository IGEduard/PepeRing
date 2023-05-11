using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    CharacterAnimatorManager characterAnimatorManager;
    CharacterWeaponSlotManager characterWeaponSlotManager;

    [Header("Lock On Transform")]
    public Transform lockOnTransform;

    [Header("Combat Colliders")]
    //public BoxCollider backStabBoxCollider;
    public CriticalDamageCollider backStabCollider;
    public CriticalDamageCollider riposteCollider;

    [Header("Interaction")]
    public bool isInteracting;

    [Header("Combat flags")]
    public bool canBeRiposted;
    public bool canBeParried;
    public bool canDoCombo;
    public bool isParrying;
    public bool isBlocking;
    public bool isInvulnerable;
    public bool isUsingRightHand;
    public bool isUsingLeftHand;
    public bool isTwoHandingWeapon;

    [Header("Movement Flags")]
    public bool isRotatingWithRootMotion;
    public bool canRotate;
    public bool isSprinting;
    public bool isInAir;
    public bool isGrounded;

    [Header("Spells")]
    public bool isFiringSpell;

    // used in backstab or riposte
    public int pendingCriticalDamage;

    protected virtual void Awake() {
        characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected virtual void FixedUpdate() {
        if (characterAnimatorManager != null)
        {
            characterAnimatorManager.CheckHandIKWeight(characterWeaponSlotManager.rightHandIKTarget, characterWeaponSlotManager.leftHandIKTarget, isTwoHandingWeapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
