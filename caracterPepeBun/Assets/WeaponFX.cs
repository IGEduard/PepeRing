using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    [Header("Weapon FX")]
    public ParticleSystem normalWeaponTrail;
    //fire weapon trail

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayWeaponFX()
    {
        normalWeaponTrail.Stop();
        if(normalWeaponTrail.isStopped)
        {
            normalWeaponTrail.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
