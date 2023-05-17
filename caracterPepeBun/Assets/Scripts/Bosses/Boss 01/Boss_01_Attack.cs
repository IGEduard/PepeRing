using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01_Attack : MonoBehaviour
{
    public GameObject ball;
    private List<GameObject> balls = new List<GameObject>();
    private GameObject[] bolls;
    public GameObject boss;
    float zBossPos1;
    float zBossPos2;
    public Vector3 bossPosition;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject test = Instantiate(ball, new Vector3(boss.transform.position.x + ((i - 5) * 5), boss.transform.position.y, boss.transform.position.z - 25), Quaternion.identity);
            balls.Add(test.gameObject);
        }
        bossPosition = boss.transform.position;
        //zBossPos1 = boss.transform.position.z - 25;
        //zBossPos1 = boss.transform.position.z + 25;
        //moveBall();
        StartCoroutine(ballMovement());
    }

    void moveBall(){
        for (int i = 0; i < 10; i++)
        {
            if (balls[i] != null)
            {
                balls[i].transform.position = Vector3.Lerp(
                    balls[i].transform.position,
                    new Vector3(balls[i].transform.position.x, balls[i].transform.position.y, balls[i].transform.position.z + 25),
                    Time.deltaTime);
                /*if (balls[i].transform.position.z < zBossPos + 50)
                {
                    balls[i].transform.position += Vector3.up * Time.deltaTime;
                } else {
                    balls[i].transform.position -= Vector3.up * Time.deltaTime;
                }*/
            }
        }
    }

    IEnumerator ballMovement(){
        while (true){
            for (int i = 0; i < balls.Count; i++)
            {
                if (balls[i] != null)
                {
                    balls[i].transform.position = Vector3.MoveTowards(
                        balls[i].transform.position,
                        new Vector3(balls[i].transform.position.x, balls[i].transform.position.y, bossPosition.z + 25*direction),
                        Time.deltaTime*10);
                }
            }
            if (Math.Abs(balls[0].transform.position.z - bossPosition.z) >= 25){
                direction *= -1;
            }
            yield return null;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //moveBall();
        //balls[3].transform.position += Vector3.up * Time.deltaTime;
    }
}
