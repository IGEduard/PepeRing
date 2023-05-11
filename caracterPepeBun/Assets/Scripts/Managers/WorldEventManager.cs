using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEventManager : MonoBehaviour
{
    public List<FogWall> fogWalls;
    public UIBossHealthBar bossHealthBar;
    public EnemyBossManager enemyBossManager;
    public bool bossFightIsActive;      // is currently fighting boss
    public bool bossHasBeenAwakened;    // woke boss but died during fight
    public bool bossHasBeenDefeated;    // boss has been defeated

    private void Awake() {
        bossHealthBar = FindObjectOfType<UIBossHealthBar>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateBossFight(string fogWallName){
        bossFightIsActive = true;
        bossHasBeenAwakened = true;
        bossHealthBar.SetUIHealthBarToActive();

        foreach (var fogWall in fogWalls)
        {
            if(fogWall.name == fogWallName){
                fogWall.ActivateFogWall();
            }
        }
    }

    public void BossHasBeenDefeated(){
        bossHasBeenDefeated = true;
        bossFightIsActive = false;
        bossHealthBar.SetUIHealthBarToInactive();
        
        foreach (var fogWall in fogWalls)
        {
            fogWall.DeactivateFogWall();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
