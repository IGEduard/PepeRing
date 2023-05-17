using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01_Movement : MonoBehaviour
{
    Vector3 bossPosition;
    Renderer bossRenderer;
    Color bossColor;
    byte r = 0;
    byte g = 0;
    byte b = 0;
    int red = 0;
    int green = 0;
    int blue = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        bossPosition = transform.position;
        bossRenderer = GetComponentInChildren<Renderer>();
        StartCoroutine(rainbowColor());
    }

    IEnumerator bossMovement(){

        yield return new WaitForSeconds(1f);
    }

    IEnumerator rainbowColor(){
        while (true)
        {
            if (r == 0 && g == 0 && b == 0){ // start red
                r = 255;
            } else if(r == 255 && g < 255 && b == 0){ // red to yellow // 255 0 0 to 255 255 0
                g++;
            } else if(r > 0 && g == 255 && b == 0){ // yellow to green // 255 255 0 to 0 255 0
                r--;
            } else if(r == 0 && g == 255 && b < 255){ // green to cyan // 0 255 0 to 0 255 255
                b++;
            } else if(r == 0 && g > 0 && b == 255){ // cyan to blue // 0 255 255 to 0 0 255
                g--;
            } else if(r < 255 && g == 0 && b == 255){ // blue to magenta // 0 0 255 to 255 0 255
                r++;
            } else if(r == 255 && g == 0 && b > 0){ // magenta to red // 255 0 255 to 255 0 0 
                b--;
            }
            bossRenderer.material.color = new Color32(r, g, b, 255);
            yield return new WaitForSeconds(checkBossColor());

        }
    }

    float checkBossColor(){
        if (r == 255){
                if (g == 255){ // 255 255 0
                    return 5f;
                } else if (b == 255){ // 255 0 255
                    return 5f;
                } else if(g == 0 && b == 0){ // 255 0 0
                    return 5f;
                } else {
                    return 0;
                }
            } else if (g == 255){ 
                if (b == 255){ // 0 255 255
                    return 5f;
                } else if (r == 0 && b == 0){ // 0 255 0
                    return 5f;
                } else {
                    return 0;
                }
            } else if(b == 255){ 
                if(r == 0 && g == 0) { // 0 0 255
                    return 5f;
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
    }

    // Update is called once per frame
    void Update()
    {
        //rainbowColor();
    }
}
