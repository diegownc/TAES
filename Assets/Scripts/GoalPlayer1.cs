using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPlayer1 : MonoBehaviour
{

    private static bool goal1;
    public static int score1;
    public Text textScore1;

    public static bool player1Respawn;
    public static bool player2Respawn;
    public static bool ballRespawn;

    void Start()
    {
        goal1 = false;
        player1Respawn = false;
        player2Respawn = false;
        ballRespawn = false;
    }

    void Update()
    {
        if (goal1)
        {
            score1++;
            goal1 = false;
        }

        textScore1.text = "" + score1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            goal1 = true;
            player1Respawn = true;
            player2Respawn = true;
            ballRespawn = true;
            //Destroy(collision.gameObject);
            //collision.transform.GetComponent<BallRespawn>().PlayerGoal();
        }
    }
}
