using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public CharacterManager characterManager;
    protected Collider damageCollider;
    public bool enabledDamageColliderOnStartUp = false;

    [Header("Poise")]
    public float poiseBreak;
    public float offensivePoiseBonus;
    
    [Header("Damage")]
    public int physicalDamage;
    public int fireDamage;
    // magicDamage;
    // lightingDamage;
    // darkDamage;

    [Header("Team ID")]
    public int teamIDNumber = 0;

    protected virtual void Awake() {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = enabledDamageColliderOnStartUp;

        //characterManager = GetComponentInParent<CharacterManager>();
    }

    public void EnableDamageCollider(){
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider(){
        damageCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Character"){
            CharacterStatsManager enemyStats = other.GetComponent<CharacterStatsManager>();
            CharacterManager enemyManager = other.GetComponent<CharacterManager>();
            CharacterEffectsManager characterEffectsManager = other.GetComponent<CharacterEffectsManager>();
            BlockingCollider shield = other.transform.GetComponentInChildren<BlockingCollider>();
            
            if(enemyManager != null){
                if(enemyStats.teamIDNumber == teamIDNumber){
                    return;
                }
                if(enemyManager.isParrying){
                    // check if parryable
                    characterManager.GetComponentInChildren<CharacterAnimatorManager>().PlayTargetAnimation("Parried", true);
                    return;
                }
                else if (shield != null && enemyManager.isBlocking){
                    float physicalDamageAfterBlock = physicalDamage - (physicalDamage * shield.blockingPhysicalDamageAbsorption) / 100;
                    float fireDamageAfterBlock = fireDamage - (fireDamage * shield.blockingFireDamageAbsorption) / 100;

                    if (enemyStats != null){
                        enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), Mathf.RoundToInt(fireDamageAfterBlock), "BlockGuard");
                        return;
                    }
                }
            }

            if(enemyStats != null){
                if(enemyStats.teamIDNumber == teamIDNumber){
                    return;
                }

                // detect where on the collider our weapon first makes contact
                Vector3 contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                characterEffectsManager.PlayBloodSplatterFX(contactPoint);

                enemyStats.poiseResetTimer = enemyStats.totalPoiseResetTime;
                enemyStats.totalPoiseDefence = enemyStats.totalPoiseDefence - poiseBreak;
                Debug.Log("The poise is currently " + enemyStats.totalPoiseDefence);

                if (false){ // is boss
                    if (enemyStats.totalPoiseDefence > poiseBreak){
                        enemyStats.TakeDamageNoAnimation(physicalDamage,fireDamage);
                        //Debug.Log("Enemy poise is currently " + enemyStats.totalPoiseDefence);
                    }
                    else {
                        enemyStats.TakeDamage(physicalDamage, fireDamage);
                        enemyStats.BreakGuard();
                    }
                }
                else {
                    if (enemyStats.totalPoiseDefence > poiseBreak){
                        enemyStats.TakeDamageNoAnimation(physicalDamage,fireDamage);
                        //Debug.Log("Enemy poise is currently " + enemyStats.totalPoiseDefence);
                    }
                    else {
                        enemyStats.TakeDamage(physicalDamage, fireDamage);
                    }
                }

                
            }
        }

        if (other.tag == "Illusionary Wall"){
            IllusionaryWall illusionaryWall = other.GetComponent<IllusionaryWall>();
            illusionaryWall.wallHasBeenHit = true;
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
