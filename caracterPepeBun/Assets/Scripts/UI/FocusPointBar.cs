using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusPointBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxFocusPoints(float maxFocusPoints){
        slider.maxValue = maxFocusPoints;
        slider.value = maxFocusPoints;
    }

    public void SetCurrentFocusPoints(float currentFocusPoints){
        slider.value = currentFocusPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
