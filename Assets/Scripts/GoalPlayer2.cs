using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPlayer2 : MonoBehaviour
{
    private static bool goal2;

    // Start is called before the first frame update
    void Start()
    {
        goal2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            goal2 = true;
        }
    }
}
