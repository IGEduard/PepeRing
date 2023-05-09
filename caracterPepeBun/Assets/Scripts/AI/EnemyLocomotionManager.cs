using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyLocomotionManager : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimatorManager;

    public CapsuleCollider characterCollider;
    public CapsuleCollider characterCollisionBlockerCollider;

    public LayerMask detectionLayer;
    
    

    private void Awake() {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
