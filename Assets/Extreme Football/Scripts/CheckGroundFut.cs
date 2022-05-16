using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGroundFut : MonoBehaviour
{

    public static bool isGrounded1;
    public static BoxCollider2D playerFloor1;
    private float sizeXizq1 = 5f;
    private float offsetXizq1 = 2.4f;
    private float sizeXder1 = 5f;
    private float offsetXder1 = -1.8f;

    void Start()
    {
        playerFloor1 = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Cambiar la size de collider de checkgroud
        if (Player1Controller.flipX_1)
        {
            playerFloor1.size = new Vector2(sizeXizq1, playerFloor1.size.y);
            playerFloor1.offset = new Vector2(offsetXizq1, playerFloor1.offset.y);
        }
        else if (!Player1Controller.flipX_1)
        {
            playerFloor1.size = new Vector2(sizeXder1, playerFloor1.size.y);
            playerFloor1.offset = new Vector2(offsetXder1, playerFloor1.offset.y);
        }

        //STOP JUMP
        if (Player1Controller.stopJump)
        {
            playerFloor1.enabled = false;
        }
        else if (!Player1Controller.stopJump)
        {
            playerFloor1.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded1 = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded1 = false;
    }
}
