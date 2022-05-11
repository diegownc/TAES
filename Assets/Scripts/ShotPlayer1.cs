using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer1 : MonoBehaviour
{

    public EdgeCollider2D shotP1;
    public Transform P;
    private float offsetFlipX = 18.85f;

    void Start()
    {
        shotP1 = GetComponent<EdgeCollider2D>();
        P = GetComponent<Transform>();
    }

    void Update()
    {
        if (Player1Controller.flipX_1)
        {
            shotP1.offset = new Vector2(offsetFlipX, shotP1.offset.y);
            P.transform.rotation = new Quaternion(P.transform.rotation.x, 180f, P.transform.rotation.z, P.transform.rotation.w);
        }
        else if (!Player1Controller.flipX_1)
        {
            shotP1.offset = new Vector2(0, shotP1.offset.y);
            P.transform.rotation = new Quaternion(P.transform.rotation.x, P.transform.rotation.y, P.transform.rotation.z, -180f);
        }

        if (Player1Controller.shot_1)
        {
            shotP1.enabled = true;
        }
        else if (!Player1Controller.shot_1)
        {
            shotP1.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (Player1Controller.flipX_1)
            {
                //IZ
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-170f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y+20);
            }
            else if (!Player1Controller.flipX_1)
            {
                //DR
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(170f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y+20);
            }
        }
    }
}
