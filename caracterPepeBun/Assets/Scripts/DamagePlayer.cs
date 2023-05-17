using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int physicalDamage = 0;
    public int fireDamage = 25;
    public int timeAmount = 3;
    float timer = 0;
    private void OnTriggerEnter(Collider other) {
        //PlayerStatsManager playerStats = other.GetComponent<PlayerStatsManager>();
        CharacterStatsManager characterStatsManager = other.GetComponent<CharacterStatsManager>();

        if(characterStatsManager != null && characterStatsManager.teamIDNumber == 0){
            characterStatsManager.TakeDamage(physicalDamage, fireDamage);
        }
    }
    private void OnTriggerStay(Collider other) {
        
        CharacterStatsManager characterStatsManager = other.GetComponent<CharacterStatsManager>();
        if(characterStatsManager != null && characterStatsManager.teamIDNumber == 0){
            if (characterStatsManager.isDead){
                return;
            }
            timer += Time.deltaTime;
            if(timer >= timeAmount){
                characterStatsManager.TakeDamage(physicalDamage, (int)(fireDamage/5));
                timer = 0;
            }
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
