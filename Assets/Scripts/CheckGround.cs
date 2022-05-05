using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGround : MonoBehaviour
{

    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.tag == "GoalLine1")
        {

        }
        if(this.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
