using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventoryManager : MonoBehaviour
{
    protected CharacterWeaponSlotManager characterWeaponSlotManager;

    [Header("Quick Slot Items")]
    public ConsumableItem currentConsumable;
    public SpellItem currentSpell;
    public WeaponItem rightWeapon;
    public WeaponItem leftWeapon;

    [Header("Current Equipment")]
    public HelmetEquipment currentHelmetEquipment;
    public TorsoEquipment currentTorsoEquipment;
    public LegEquipment currentLegEquipment;
    public HandEquipment currentHandEquipment;

    //public WeaponItem unarmedWeapon;

    public WeaponItem[] weaponsInRightHandSlots = new WeaponItem[1];
    public WeaponItem[] weaponsInLeftHandSlots = new WeaponItem[1];

    public int currentRightWeaponIndex = -1;
    public int currentLeftWeaponIndex = -1;

    private void Awake() {
        characterWeaponSlotManager = GetComponent<CharacterWeaponSlotManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        characterWeaponSlotManager.LoadBothWeaponsOnSlots();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
