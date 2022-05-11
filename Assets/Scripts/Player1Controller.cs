using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    bool canJump2 = false;

    public float runSpeed = 55;
    public float jumpSpeed = 70;
    public float jumpSpeed2 = 50;
    Rigidbody2D rb2d;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 10f;

    public SpriteRenderer spriterederer;
    public Animator animator;
    public Transform posPlayer1;
    //public BoxCollider2D player;

    public static bool flipX_1;
    public static bool shot_1;
    public static bool stopJump = false;
    //private float offsetXder1 = -3.8f;

    void Start()
    {
        spriterederer = GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        posPlayer1 = GetComponent<Transform>();
        //player = GetComponent<BoxCollider2D>();
        gameObject.transform.position = new Vector2(-58, -33);
    }

    private void Update()
    {
        //DOBLE SALTO

        if (Input.GetKey("w"))
        {
            if (CheckGround.isGrounded1)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
                canJump2 = true;
            }
            else
            {
                if (Input.GetKeyDown("w"))
                {
                    if (canJump2)
                    {
                        if (!stopJump) //Este no hace nada, ver como arreglar o dejar así, al menos no peta.
                        {
                            animator.SetBool("Jump", true);
                        }
                        
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed2);
                        canJump2 = false;
                    }
                }
            }
        }

        if (!stopJump)
        {
            if (!CheckGround.isGrounded1)
            {
                animator.SetBool("Jump", true);
                animator.SetBool("Run", false);
            }
            else if (CheckGround.isGrounded1)
            {
                animator.SetBool("Jump", false);
            }
        }

        //STOP JUMP

        if(posPlayer1.position.x <= -68)
        {
            stopJump = true;
        }
        else if(posPlayer1.position.x > -68)
        {
            stopJump = false;
        }
    }

    void FixedUpdate()
    {
        //Movimiento
        if (Input.GetKey("a"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            spriterederer.flipX = true;
            flipX_1 = true;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey("d"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
            spriterederer.flipX = false;
            flipX_1 = false;
            animator.SetBool("Run", true);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            animator.SetBool("Run", false);
        }

        //Disparo
        if (Input.GetKey("space"))
        {
            animator.SetBool("Shot", true);
            shot_1 = true;
            //player.offset = new Vector2(offsetXder1, player.offset.y);
        }
        else
        {
            animator.SetBool("Shot", false);
            shot_1 = false;
        }

        /**if(rb2d.velocity.y < 0.1f && rb2d.velocity.y > -0.1f){
            CheckGround.isGrounded1 = true;
        }**/

        if (betterJump)
        {
            if (rb2d.velocity.y < 0)
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
            }
            if (rb2d.velocity.y > 0 && !Input.GetKey("w"))
            {
                rb2d.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier) * Time.deltaTime;
            }
        }

        if (GoalPlayer1.player1Respawn || GoalPlayer2.player1Respawn2)
        {
            gameObject.transform.position = new Vector2(-58, -33);
            rb2d.velocity = new Vector2(0, 0);
            GoalPlayer1.player1Respawn = false;
            GoalPlayer2.player1Respawn2 = false;
        }
    }
}