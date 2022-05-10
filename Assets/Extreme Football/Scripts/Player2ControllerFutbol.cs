using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ControllerFutbol : MonoBehaviour
{
    bool canJump2_2 = false;

    public float runSpeed2 = 55;
    public float jumpSpeed2 = 70;
    //public float jumpNotSpeed = 50;
    Rigidbody2D rb2d2;

    public bool betterJump2 = false;
    public float fallMultiplier2 = 0.5f;
    public float lowJumpMultiplier2 = 10f;

    public SpriteRenderer spriterederer2;
    public Animator animator2;

    // Start is called before the first frame update
    void Start()
    {
        rb2d2 = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector2(58, -33);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("left"))
        {
            rb2d2.velocity = new Vector2(-runSpeed2, rb2d2.velocity.y);
            spriterederer2.flipX = true;
            animator2.SetBool("Run2", true);
        }
        else if (Input.GetKey("right"))
        {
            rb2d2.velocity = new Vector2(runSpeed2, rb2d2.velocity.y);
            spriterederer2.flipX = false;
            animator2.SetBool("Run2", true);
        }
        else
        {
            rb2d2.velocity = new Vector2(0, rb2d2.velocity.y);
            animator2.SetBool("Run2", false);
        }

        //DOBLE SALTO
        if (!VerificarSuelo.isGrounded && Input.GetKeyDown("up") && canJump2_2)
        {
            rb2d2.velocity = new Vector2(rb2d2.velocity.x, jumpSpeed2);
            canJump2_2 = false;
        }

        if (Input.GetKeyDown("up") && VerificarSuelo.isGrounded)
        {
            rb2d2.velocity = new Vector2(rb2d2.velocity.x, jumpSpeed2);
            canJump2_2 = true;
        }

        if (!VerificarSuelo.isGrounded)
        {
            animator2.SetBool("Jump2", true);
            animator2.SetBool("Run2", false);
        }
        else if (VerificarSuelo.isGrounded)
        {
            animator2.SetBool("Jump2", false);
        }

        if (Input.GetKey("m"))
        {
            animator2.SetBool("Shot2", true);
        }
        else
        {
            animator2.SetBool("Shot2", false);
        }

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
    }
}