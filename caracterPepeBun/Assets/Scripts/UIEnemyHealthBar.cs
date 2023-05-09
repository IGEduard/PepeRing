using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIEnemyHealthBar : MonoBehaviour
{
    Slider slider;
    float timeUntilBarIsHidden = 0;
    //Camera mainCamera;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();

    }

    public void SetHealth(int health)
    {
        slider.value = health;
        timeUntilBarIsHidden = 5;
    }

    public void SetMaxHealth(int maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timeUntilBarIsHidden = timeUntilBarIsHidden - Time.deltaTime;

        if (slider != null)
        {
            if (timeUntilBarIsHidden <= 0)
            {
                timeUntilBarIsHidden = 0;
                slider.gameObject.SetActive(false);
            }
            else
            {
                if (!slider.gameObject.activeInHierarchy)
                {
                    slider.gameObject.SetActive(true);
                }
            }

            if (slider.value <= 0)
            {
                Destroy(slider.gameObject);
            }
        }

    }

    private void LateUpdate() {
        if(slider != null)
        {
            //transform.forward = mainCamera.transform.forward;
            transform.LookAt(Camera.main.transform);
        }
    }
}
