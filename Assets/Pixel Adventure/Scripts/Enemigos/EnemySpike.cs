using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player1"))
        {
            collision.gameObject.GetComponent<PlayerMove>().Morir();
        } else if (collision.transform.CompareTag("Player2"))
        {
            collision.gameObject.GetComponent<PlayerMove2>().Morir();
        }
    }
}
