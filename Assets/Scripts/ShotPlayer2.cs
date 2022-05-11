using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPlayer2 : MonoBehaviour
{

    public EdgeCollider2D shotP2;
    public Transform P2;
    private float offsetFlipX = 18.85f;

    void Start()
    {
        shotP2 = GetComponent<EdgeCollider2D>();
        P2 = GetComponent<Transform>();
    }

    void Update()
    {
        if (!Player2Controller.flipX_2)
        {
            shotP2.offset = new Vector2(offsetFlipX, shotP2.offset.y);
            P2.transform.rotation = new Quaternion(P2.transform.rotation.x, P2.transform.rotation.y, P2.transform.rotation.z, -180f);
        }
        else if (Player2Controller.flipX_2)
        {
            shotP2.offset = new Vector2(0, shotP2.offset.y);
            P2.transform.rotation = new Quaternion(P2.transform.rotation.x, 180f, P2.transform.rotation.z, P2.transform.rotation.w);
        }

        if (Player2Controller.shot_2)
        {
            shotP2.enabled = true;
        }
        else if (!Player2Controller.shot_2)
        {
            shotP2.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            if (Player2Controller.flipX_2)
            {
                //IZ
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-170f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y+20);
            }
            else if (!Player2Controller.flipX_2)
            {
                //DR
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(170f, collision.gameObject.GetComponent<Rigidbody2D>().velocity.y+20);
            }
        }
    }
}