using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Enemy Action/Attack Action")]
public class EnemyAttackAction : EnemyAction
{
    public bool canCombo;

    public EnemyAttackAction comboAction;
    public int attackScore = 3;
    public float recoveryTime = 2;

    public float maximumAttackAngle = 35;
    public float minimumAttackAngle = -35;

    public float maximumDistanceNeededToAttack = 3;
    public float minimumDistanceNeedenToAttack = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
