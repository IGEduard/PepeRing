using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockingCollider : MonoBehaviour
{
    public BoxCollider blockingCollider;

    public float blockingPhysicalDamageAbsorption;
    public float blockingFireDamageAbsorption;

    private void Awake() {
        blockingCollider = GetComponent<BoxCollider>();
    }

    public void SetColliderDamageAbsorption(WeaponItem weapon){
        if(weapon != null){
            blockingPhysicalDamageAbsorption = weapon.physicalDamageAbsorption;
            blockingFireDamageAbsorption = weapon.fireDamageAbsorption;
        }
    }
    public void EnableBlockingCollider(){
        blockingCollider.enabled = true;
    }
    public void DisableBlockingCollider(){
        blockingCollider.enabled = false;
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
