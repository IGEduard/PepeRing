using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PoisonAmountBar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 100;
        slider.value = 100;
        gameObject.SetActive(false);
    }

    public void SetPoisonAmount(int currentPoisonAmount){
        slider.value = currentPoisonAmount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
