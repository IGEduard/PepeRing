using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_01_Attack : MonoBehaviour
{
    public GameObject ball;
    private List<GameObject> balls = new List<GameObject>();
    private GameObject[] bolls;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject test = Instantiate(ball, new Vector3(boss.transform.position.x + ((i - 5) * 5), boss.transform.position.y, boss.transform.position.z), Quaternion.identity);
            balls.Add(test.gameObject);
        }
        //moveBall();
        //StartCoroutine(ballMovement());
    }

    void moveBall(){
        for (int i = 0; i < 10; i++)
        {
            if (balls[i] != null)
            {
                balls[i].transform.position += Vector3.up * Time.deltaTime;
            }
        }
    }

    IEnumerator ballMovement(){
        for (int i = 0; i < 10; i++)
        {
            balls[i].transform.position += Vector3.up * Time.deltaTime;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        moveBall();
        //balls[3].transform.position += Vector3.up * Time.deltaTime;
    }
}
