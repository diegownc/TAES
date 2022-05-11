using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRight : MonoBehaviour
{
    public static bool isRight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger)
            isRight = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isRight = false;
    }
}
