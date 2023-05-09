using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDamageCollider : DamageCollider
{
    [Header("Explosive Damage & Radius")]
    public int explosiveRadius = 1;
    public int explosionDamage;
    public int explosionSplashDamage;
    // magicExplosionDamage;
    // lightingExplosionDamage;

    public Rigidbody bombRigidBody;
    private bool hasCollided;
    public GameObject impactParticles;

    protected override void Awake()
    {
        //base.Awake();
        damageCollider = GetComponent<Collider>();
        bombRigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision other) {
        if (!hasCollided){
            hasCollided = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.identity);
            Explode();

            CharacterStatsManager character = other.transform.GetComponent<CharacterStatsManager>();

            if (character != null){
                // check for friendly fire
                if (character.teamIDNumber != teamIDNumber){
                    character.TakeDamage(0, explosionDamage);
                }
            }

            Destroy(impactParticles, 5f);
            Destroy(transform.parent.parent.gameObject);
        }
    }

    private void Explode(){
        Collider[] characters = Physics.OverlapSphere(transform.position, explosiveRadius);
        
        foreach (Collider objectsInExplosion in characters){
            CharacterStatsManager character = objectsInExplosion.GetComponent<CharacterStatsManager>();
            if (character != null){
                // deal fire damage
                if (character.teamIDNumber != teamIDNumber){
                    character.TakeDamage(0, explosionSplashDamage);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
