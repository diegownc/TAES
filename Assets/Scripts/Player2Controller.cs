using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    bool canJump2_2 = false;

    public float runSpeed2 = 55;
    public float jumpSpeed2 = 70;
    public float jumpSpeed2_2 = 50;
    Rigidbody2D rb2d2;

    public bool betterJump2 = false;
    public float fallMultiplier2 = 0.5f;
    public float lowJumpMultiplier2 = 10f;

    public SpriteRenderer spriterederer2;
    public Animator animator2;
    public BoxCollider2D player2;

    public static bool flipX_2;
    public static bool shot_2;

    void Start()
    {
        spriterederer2 = GetComponent<SpriteRenderer>();
        rb2d2 = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector2(58, -33);
    }

    private void Update()
    {
        //DOBLE SALTO
        if (Input.GetKey("up"))
        {
            if (CheckGround2.isGrounded2)
            {
                rb2d2.velocity = new Vector2(rb2d2.velocity.x, jumpSpeed2);
                canJump2_2 = true;
            }
            else
            {
                if (Input.GetKeyDown("up"))
                {
                    if (canJump2_2)
                    {
                        animator2.SetBool("Jump2", true);
                        rb2d2.velocity = new Vector2(rb2d2.velocity.x, jumpSpeed2_2);
                        canJump2_2 = false;
                    }
                }
            }
        }

        if (!CheckGround2.isGrounded2)
        {
            animator2.SetBool("Jump2", true);
            animator2.SetBool("Run2", false);
        }
        else if (CheckGround2.isGrounded2)
        {
            animator2.SetBool("Jump2", false);
        }
    }

    void FixedUpdate()
    {
        //Movimiento
        if (Input.GetKey("left"))
        {
            rb2d2.velocity = new Vector2(-runSpeed2, rb2d2.velocity.y);
            spriterederer2.flipX = true;
            flipX_2 = true;
            animator2.SetBool("Run2", true);
        }
        else if (Input.GetKey("right"))
        {
            rb2d2.velocity = new Vector2(runSpeed2, rb2d2.velocity.y);
            spriterederer2.flipX = false;
            flipX_2 = false;
            animator2.SetBool("Run2", true);
        }
        else
        {
            rb2d2.velocity = new Vector2(0, rb2d2.velocity.y);
            animator2.SetBool("Run2", false);
        }

        //Disparo
        if (Input.GetKey("m"))
        {
            animator2.SetBool("Shot2", true);
            shot_2 = true;
        }
        else
        {
            animator2.SetBool("Shot2", false);
            shot_2 = false;
        }

        /**if (rb2d2.velocity.y < 0.1f && rb2d2.velocity.y > -0.1f)
        {
            CheckGround2.isGrounded2 = true;
        }**/

        if (betterJump2)
        {
            if (rb2d2.velocity.y < 0)
            {
                rb2d2.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier2) * Time.deltaTime;
            }
            if (rb2d2.velocity.y > 0 && !Input.GetKey("up"))
            {
                rb2d2.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier2) * Time.deltaTime;
            }
        }

        if (GoalPlayer1.player2Respawn || GoalPlayer2.player2Respawn2)
        {
            gameObject.transform.position = new Vector2(58, -33);
            rb2d2.velocity = new Vector2(0, 0);
            GoalPlayer1.player2Respawn = false;
            GoalPlayer2.player2Respawn2 = false;
        }
    }
}