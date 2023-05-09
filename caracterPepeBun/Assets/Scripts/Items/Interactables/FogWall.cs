using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogWall : MonoBehaviour
{
    private void Awake() {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateFogWall(){
        gameObject.SetActive(true);
    }

    public void DeactivateFogWall(){
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
