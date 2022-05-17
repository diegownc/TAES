using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
    public Transform posPlayer2;
    //public BoxCollider2D player2;

    public static bool flipX_2;
    public static bool shot_2;
    public static bool stopJump2;

    Thread telefono;
    static UdpClient udp;

    public bool moverDerecha;
    public bool moverIzquierda;
    public bool saltarTelefono;
    public bool chutarTelefono;

    private void OnDestroy()
    {
        udp.Close();
    }

    void Start()
    {
        spriterederer2 = GetComponent<SpriteRenderer>();
        rb2d2 = GetComponent<Rigidbody2D>();
        posPlayer2 = GetComponent<Transform>();
        gameObject.transform.position = new Vector2(58, -33);

        moverDerecha = false;
        moverIzquierda = false;
        saltarTelefono = false;
        chutarTelefono = false;
        udp = new UdpClient(8052);
        telefono = new Thread(new ThreadStart(ThreadMethod));
        telefono.Start();
    }

    private void Update()
    {
        //DOBLE SALTO
        if (Input.GetKey("up") || saltarTelefono)
        {
            
            if (CheckGroundFut2.isGrounded2)
            {
                rb2d2.velocity = new Vector2(rb2d2.velocity.x, jumpSpeed2);
                canJump2_2 = true;
            }else
            {
                if (Input.GetKeyDown("up") || saltarTelefono)
                {
                    if (canJump2_2)
                    {
                        if (!stopJump2) //Este no hace nada, ver como arreglar o dejar as�, al menos no peta.
                        {
                            animator2.SetBool("Jump2", true);
                        }

                        rb2d2.velocity = new Vector2(rb2d2.velocity.x, jumpSpeed2_2);
                        canJump2_2 = false;
                    }
                }
            }
        }

        if (!stopJump2)
        {
            if (!CheckGroundFut2.isGrounded2)
            {
                animator2.SetBool("Jump2", true);
                animator2.SetBool("Run2", false);
            }
            else if (CheckGroundFut2.isGrounded2)
            {
                animator2.SetBool("Jump2", false);
            }
        }

        //STOP JUMP

        if (posPlayer2.position.x >= 68)
        {
            stopJump2 = true;
        }
        else if (posPlayer2.position.x < 68)
        {
            stopJump2 = false;
        }
    }

    void FixedUpdate()
    {
        //Movimiento
        if (Input.GetKey("left") || moverIzquierda)
        {
            rb2d2.velocity = new Vector2(-runSpeed2, rb2d2.velocity.y);
            spriterederer2.flipX = true;
            flipX_2 = true;
            animator2.SetBool("Run2", true);
        }
        else if (Input.GetKey("right") || moverDerecha)
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
        if (Input.GetKey("p") || chutarTelefono)
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
            CheckGroundFut2.isGrounded2 = true;
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

        if (TimeController.finPartido)
        {
            Destroy(gameObject);
        }
    }

    private void ThreadMethod()
    {
        //Obtengo la direcci�n IP de este pc
        string localIP = string.Empty;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }

        Debug.Log(localIP);
        Debug.Log("VOY A EMPEZAR A RECIBIR MENSAJES");
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(localIP), 0);
            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);
            string cadena = Encoding.UTF8.GetString(receiveBytes);

            if (cadena.Contains("Right"))
            {
                moverDerecha = true;
                if (cadena.Contains("F"))
                    moverDerecha = false;
            }

            if (cadena.Contains("Left"))
            {
                moverIzquierda = true;
                if (cadena.Contains("F"))
                    moverIzquierda = false;
            }

            if (cadena.Contains("Jump"))
            {
                saltarTelefono= true;
                if (cadena.Contains("F"))
                    saltarTelefono = false;
            }

            if (cadena.Contains("Shot"))
            {
                chutarTelefono = true;

                if (cadena.Contains("F"))
                    chutarTelefono = false;
            }
        }
    }
}