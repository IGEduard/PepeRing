using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventColliderBeginBossFight : MonoBehaviour
{
    WorldEventManager worldEventManager;
    private void Awake() {
        worldEventManager = FindObjectOfType<WorldEventManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Character"){
            worldEventManager.ActivateBossFight(name);
            Destroy(this); // new
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
