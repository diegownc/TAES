using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRight2 : MonoBehaviour
{
    public static bool isRight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.isTrigger && !collision.CompareTag("Enemigo"))
            isRight = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isRight = false;
    }
}
