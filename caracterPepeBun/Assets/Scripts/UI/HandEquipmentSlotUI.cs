using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandEquipmentSlotUI : MonoBehaviour
{
    UIManager uIManager;
    public Image icon;
    WeaponItem weapon;

    public bool rightHandSlot01;
    public bool rightHandSlot02;

    public bool leftHandSlot01;
    public bool leftHandSlot02;

    private void Awake() {
        uIManager = FindObjectOfType<UIManager>();
    }

    public void AddItem(WeaponItem newWeapon){
        weapon = newWeapon;
        icon.sprite = weapon.itemIcon;
        icon.enabled = true;
        gameObject.SetActive(true);
    }

    public void ClearItem(){
        weapon = null;
        icon.sprite = null;
        icon.enabled = false;
        gameObject.SetActive(false);
    }

    public void SelectThisSlot(){
        if (rightHandSlot01)
        {
            uIManager.rightHandSlot01Selected = true;
        }
        else if (rightHandSlot02)
        {
            uIManager.rightHandSlot02Selected = true;
        }
        else if (leftHandSlot01)
        {
            uIManager.leftHandSlot01Selected = true;
        }
        else
        {
            uIManager.leftHandSlot02Selected = true;
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
