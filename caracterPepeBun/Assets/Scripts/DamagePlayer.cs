using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int physicalDamage = 25;
    public int fireDamage = 25;
    private void OnTriggerEnter(Collider other) {
        PlayerStatsManager playerStats = other.GetComponent<PlayerStatsManager>();

        if(playerStats != null){
            playerStats.TakeDamage(physicalDamage, fireDamage);
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
