using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulCountBar : MonoBehaviour
{
    public Text soulCountText;

    public void SetSoulCountText(int soulCount){
        soulCountText.text = soulCount.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
