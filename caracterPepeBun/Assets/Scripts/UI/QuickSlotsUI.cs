using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotsUI : MonoBehaviour
{
    public Image currentSpellIcon;
    public Image currentConsumableIcon;
    public Image leftWeaponIcon;
    public Image rightWeaponIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon){
        if (isLeft == false){
            if(weapon.itemIcon != null){
                rightWeaponIcon.sprite = weapon.itemIcon;
                rightWeaponIcon.enabled = true;
            } else {
                rightWeaponIcon.sprite = null;
                rightWeaponIcon.enabled = false;
            }          
        } else {
            if(weapon.itemIcon != null){
                leftWeaponIcon.sprite = weapon.itemIcon;
                leftWeaponIcon.enabled = true;
            } else {
                leftWeaponIcon.sprite = null;
                leftWeaponIcon.enabled = false;
            }
        }
    }

    public void UpdateCurrentSpellIcon(SpellItem spell){
        if(spell.itemIcon != null){
            currentSpellIcon.sprite = spell.itemIcon;
            currentSpellIcon.enabled = true;
        } else {
            currentSpellIcon.sprite = null;
            currentSpellIcon.enabled = false;
        }   
    }

    public void UpdateCurrentConsumableItem(ConsumableItem consumable){
        if(consumable.itemIcon != null){
            currentSpellIcon.sprite = consumable.itemIcon;
            currentSpellIcon.enabled = true;
        } else {
            currentSpellIcon.sprite = null;
            currentSpellIcon.enabled = false;
        }  
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
