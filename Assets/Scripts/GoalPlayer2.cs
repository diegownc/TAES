using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalPlayer2 : MonoBehaviour
{
    private static bool goal2;
    public static int score2;
    public Text textScore2;

    public static bool player1Respawn2;
    public static bool player2Respawn2;
    public static bool ballRespawn2;

    // Start is called before the first frame update
    void Start()
    {
        goal2 = false;
        player1Respawn2 = false;
        player2Respawn2 = false;
        ballRespawn2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (goal2)
        {
            score2++;
            goal2 = false;
        }

        textScore2.text = "" + score2;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            goal2 = true;
            player1Respawn2 = true;
            player2Respawn2 = true;
            ballRespawn2 = true;
            //Destroy(collision.gameObject);
            //collision.transform.GetComponent<BallRespawn>().PlayerGoal();
        }
    }
}
