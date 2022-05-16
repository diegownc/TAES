using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUp : MonoBehaviour
{
    public static bool isUp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isUp = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isUp = false;
    }
}
