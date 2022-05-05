using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    bool canJump2 = false;

    public float runSpeed = 55;
    public float jumpSpeed = 70;
    //public float jumpNotSpeed = 50;
    Rigidbody2D rb2d;

    public bool betterJump = false;
    public float fallMultiplier = 0.5f;
    public float lowJumpMultiplier = 10f;

    public SpriteRenderer spriterederer;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector2(-58, -33);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);
            spriterederer.flipX = true;
            animator.SetBool("Run", true);
        }
        else if (Input.GetKey("d"))
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);
            spriterederer.flipX = false;
            animator.SetBool("Run", true);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            animator.SetBool("Run", false);
        }

        //DOBLE SALTO
        if(!CheckGround.isGrounded && Input.GetKeyDown("w") && canJump2)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            canJump2 = false;
        }

        else if (Input.GetKeyDown("w") && CheckGround.isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpSpeed);
            canJump2 = true;
        }

        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Run", false);
        }
        else if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
        }

        if (Input.GetKey("f"))
        {
            animator.SetBool("Shot", true);
        }
        else
        {
            animator.SetBool("Shot", false);
        }

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
    }
}