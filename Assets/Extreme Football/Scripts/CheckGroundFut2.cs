using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundFut2 : MonoBehaviour
{

    public static bool isGrounded2;
    public static BoxCollider2D playerFloor2;
    private float sizeXizq = 5f;
    private float offsetXizq = 1.9f;
    private float sizeXder = 5f;
    private float offsetXder = -1.4f;

    void Start()
    {
        playerFloor2 = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Cambiar la size de collider de checkgroud
        if (Player2Controller.flipX_2)
        {
            playerFloor2.size = new Vector2(sizeXizq, playerFloor2.size.y);
            playerFloor2.offset = new Vector2(offsetXizq, playerFloor2.offset.y);
        }
        else if (!Player2Controller.flipX_2)
        {
            playerFloor2.size = new Vector2(sizeXder, playerFloor2.size.y);
            playerFloor2.offset = new Vector2(offsetXder, playerFloor2.offset.y);
        }

        //STOP JUMP
        if (Player2Controller.stopJump2)
        {
            playerFloor2.enabled = false;
        }
        else if (!Player2Controller.stopJump2)
        {
            playerFloor2.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded2 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded2 = false;
    }
}
