using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoisonBuildUpBar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 100;
        slider.value = 0;
        gameObject.SetActive(false);
    }

    public void SetPoisonBuildUpAmount(int currentPoisonBuildUp){
        slider.value = currentPoisonBuildUp;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
