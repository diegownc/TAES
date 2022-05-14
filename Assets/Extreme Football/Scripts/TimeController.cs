using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    [SerializeField] int min, seg;
    [SerializeField] Text textTime;


    private float timeRest;
    private bool run;
    public static bool finPartido;

    private void Awake()
    {
        timeRest = (min * 60) + seg;
        run = true;
        finPartido = false;
    }

    void Update()
    {
        if (run)
        {
            timeRest -= Time.deltaTime;
            
            if(timeRest <= 0)
            {
                timeRest += Time.deltaTime;
                run = false;
                finPartido = true;
            }

            int timeMin = Mathf.FloorToInt(timeRest / 60);
            int timeSeg = Mathf.FloorToInt(timeRest % 60);
            textTime.text = string.Format("{00:00}:{01:00}", timeMin, timeSeg);
        }
    }
}
