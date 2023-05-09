using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellDamageCollider : DamageCollider
{
    public GameObject impactParticles;
    public GameObject projectileParticles;
    public GameObject muzzleParticles;

    bool hasCollided = false;

    CharacterStatsManager spellTarget;
    Rigidbody rigidBody;

    Vector3 impactNormal; // used to rotate the impact particles

    protected override void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        projectileParticles = Instantiate(projectileParticles, transform.position, transform.rotation);
        projectileParticles.transform.parent = transform;

        if (muzzleParticles){
            muzzleParticles = Instantiate(muzzleParticles, transform.position, transform.rotation);
            Destroy(muzzleParticles, 2f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(!hasCollided){
            spellTarget = other.transform.GetComponent<CharacterStatsManager>();

            if (spellTarget != null && spellTarget.teamIDNumber != teamIDNumber){
                spellTarget.TakeDamage(0, fireDamage);
            }
            hasCollided = true;
            impactParticles = Instantiate(impactParticles, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
        
            Destroy(projectileParticles);
            Destroy(impactParticles, 1f);
            Destroy(gameObject, 2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
