using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class NotaController : MonoBehaviour
{
    Random aleatorio = new Random();
    int v = 1;
    int flag = 0;

    public void parar()
    {
        flag = 1;
        gameObject.GetComponent<Transform>().position = new Vector3(800, 288, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Transform>().position = new Vector3(800, 288, -2);
    }

    // Update is called once per frame
    void Update()
    {

        if (gameObject.transform.position.x < -800 && flag==0)
        {
            gameObject.GetComponent<Transform>().position = new Vector3(800, 288, -2);
            int i = aleatorio.Next(1, 100);
            if (i < 40)
            {
                v = 1;
            }
            else if(i>40 && i < 70)
            {
                v = 2;
            }
            else if (i>70 && i< 90)
            {
                v = 3;
            }
            else
            {
                v = 10;
            }
        }
        gameObject.GetComponent<Transform>().position = new Vector3(gameObject.transform.position.x - v, gameObject.transform.position.y, gameObject.transform.position.z);
    }     
}
