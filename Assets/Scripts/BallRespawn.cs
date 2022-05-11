using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallRespawn : MonoBehaviour
{
    Rigidbody2D rbBall;

    void Start()
    {
        gameObject.transform.position = new Vector2(0, 0);
        rbBall = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (GoalPlayer1.ballRespawn || GoalPlayer2.ballRespawn2)
        {
           gameObject.transform.position = new Vector2(0, 0);
           rbBall.velocity = new Vector2(0, 0);
           GoalPlayer1.ballRespawn = false;
           GoalPlayer2.ballRespawn2 = false;
        }
    }

    /**public void PlayerGoal()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }**/
}
