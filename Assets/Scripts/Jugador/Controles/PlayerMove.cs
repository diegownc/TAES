using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Posision inicial de personaje
    //public Vector2 Spawn = new Vector2(-1, 1);
    
    //Sockets
    static UdpClient udp;
    Thread thread;
    private bool moverDerecha = false;
    private bool moverIzquierda = false;
    private bool saltar = false;
    private bool movil = false;

    //Controles
    public float horizontalInput;

    //Atributos movimiento
    public float velocidad = 1f;
    public float fuerzaSaltoSimple = 17f;
    public float elevacionDobleSalto = 3;

    //Estados del personaje
    //Terreno
    private bool suelo = false;
    private bool cayendo = true;
    private bool saltando = false;
    //Personaje
    public bool dead = false;
    private bool muriendo = false;
    private bool reaparecer = true;
    private bool reapareciendo = true;
    private bool spawn = true;
    //Jugabilidad
    private bool segundoSaltoDisponible = false;
    public bool segundoSaltoActivo = false;

    //Atributos de metodos
    private double frameSalto = 0;
    private double frameReaparecer = 0;
    private float tiempoCaer = 0.95f;
    private float tiempoReaparecer = 0.4f;
    private float tiempoDobleSalto = 0.002f;

    private void Start()
    {
        frameReaparecer = transform.position.y;

        if (movil)
        {
            udp = new UdpClient(8055);
            thread = new Thread(new ThreadStart(ThreadMethod));
            thread.Start();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (reapareciendo)
        {
            AnimacionEnAire();
            tiempoCaer = tiempoCaer - Time.deltaTime;
            Reaparecer();
        }
        else if (dead)
        {
            Morir();
        }
        else if (muriendo)
        {
            tiempoReaparecer = tiempoReaparecer - Time.deltaTime;
            PlayerSpawn();
        }
        else
        {
            //Controles derecha izquierda y animaciones correspondientes
            MovimientoHorizontal();

            //Control de Salto
            Salto();

            //Detecion de caidas y saltos
            ActualizacionDeEstados();

            //Transiciones de estados en el aire
            ComportamiendoEnAire();

            //ComportamiendoEnAire();
            AnimacionEnAire();
        }
    }


    private void ActualizacionDeEstados()
    {
        //si cae que cambie 
        //salto simple
        if (reaparecer && frameReaparecer > transform.position.y + 2)
        {
            reaparecer = false;
            cayendo = true;
        }

        if (CheckGround.isGround)
        {
            suelo = true;
            cayendo = false;
            reaparecer = false;
            segundoSaltoActivo = false;
            //saltando = false;
        }
        else
        {
            //Si se choca con algun techo o plataforma
            if (CheckUp.isUp)
            {
                saltando = false;
                cayendo = true;
            }
            else
            {
                suelo = false;
                if (!saltando)
                    cayendo = true;
            }
        }
    }

    private void ComportamiendoEnAire()
    {
        if (!suelo && !segundoSaltoActivo)
        {
            //un poco chapuza, corregir en cuanto se encuentre otra forma mas simple
            if (saltando && gameObject.transform.position.y > 0.5 + frameSalto)
            {
                saltando = false;
                cayendo = true;
            }
        }
        else if (!suelo && segundoSaltoActivo)
        {
            
            //un poco chapuza, corregir en cuanto se encuentre otra forma mas simple
            if (segundoSaltoActivo && gameObject.transform.position.y  > 0.4 + frameSalto ) //0.3
            {
                segundoSaltoActivo = false;
                cayendo = true;
            }
        }
    }

    private void AnimacionEnAire()
    {
        if (reaparecer)
        {
            gameObject.GetComponent<Animator>().SetBool("Reaparecer", true);
        } else  gameObject.GetComponent<Animator>().SetBool("Reaparecer", false);
        
        if (cayendo)
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", true);
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
        }
        else if (saltando)
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", false);
            gameObject.GetComponent<Animator>().SetBool("Jump", true);
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", false);
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
            gameObject.GetComponent<Animator>().SetBool("DobleSalto", false);
        }
    }

    private void MovimientoHorizontal()
    {
        if (!movil)
        {
            //Recogiendo datos y potencia del teclado (solo Horinzontal, vertial no hace falta, el salto es fijo)
            horizontalInput = Input.GetAxis("Horizontal");
        }
        else
        {
            if (moverDerecha)
            {
                horizontalInput = 0.5f;
            }
            else if (moverIzquierda)
            {
                horizontalInput = -0.5f;
            }
        }

        if (horizontalInput > 0.01 || horizontalInput < -0.01)
        {
            if (horizontalInput < -0.01)
            {
                //simetria del pesonaje
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else gameObject.GetComponent<SpriteRenderer>().flipX = false;

            
            if (!(horizontalInput < -0.01 && CheckLeft.isLeft) && !(horizontalInput > 0.01 && CheckRight.isRight))
                //velocidad * Direcion(direcion de la fuerza) * Fuerza * DeltaTime(constante, para controlar los fps)
                transform.Translate(velocidad * Time.deltaTime * Vector3.right * horizontalInput);

            gameObject.GetComponent<Animator>().SetBool("Run", true);
        }
        else gameObject.GetComponent<Animator>().SetBool("Run", false);
    }

    private void Salto()
    {
        if (movil)
        {
            if (saltar)
            {
                //verticalInput = Input.GetAxis("Vertical");
                if (suelo)
                {
                    saltando = true;
                    frameSalto = gameObject.transform.position.y;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSaltoSimple));
                    segundoSaltoDisponible = true;
                }
                else if ( segundoSaltoDisponible)
                {
                    segundoSaltoDisponible = false;
                    saltando = false;
                    segundoSaltoActivo = true;
                    GameObject.Find("SpawnJumpEffect").GetComponent<SpawnJumpEffect>().Spawn(transform.position);
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, elevacionDobleSalto);
                    frameSalto = gameObject.transform.position.y;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown("w")){
                //verticalInput = Input.GetAxis("Vertical");
                if (suelo)
                {
                    saltando = true;
                    frameSalto = gameObject.transform.position.y;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSaltoSimple));
                    segundoSaltoDisponible = true;
                }
                else if (segundoSaltoDisponible)
                {
                    segundoSaltoDisponible = false;
                    saltando = false;
                    segundoSaltoActivo = true;
                    GameObject.Find("SpawnJumpEffect").GetComponent<SpawnJumpEffect>().Spawn(transform.position);
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, elevacionDobleSalto);
                    frameSalto = gameObject.transform.position.y;
                }
            }
        }
    }

    private void Morir()
    {
        dead = false;
        muriendo = true;
        gameObject.GetComponent<Animator>().SetBool("Reaparecer", false);
        gameObject.GetComponent<Animator>().SetBool("Run", false);
        gameObject.GetComponent<Animator>().SetBool("Fall", false);
        gameObject.GetComponent<Animator>().SetBool("Jump", false);
        gameObject.GetComponent<Animator>().SetBool("Morrir", true);
        PlayerPrefs.SetInt("Vidas", PlayerPrefs.GetInt("Vidas") - 1);
        ResetCheck();
        if(movil)
            udp.Close();
        Destroy(gameObject, 0.5f);
    }
    
    private void PlayerSpawn()
    {
        if (tiempoReaparecer <= 0 && spawn)
        {
            spawn = false;
            GameObject.Find("SpawnPlayerFrog").GetComponent<SpawnPlayer>().Spawn();
        }
    }

    private void ResetCheck()
    {
        CheckGround.isGround = false;
        CheckUp.isUp = false;
        CheckLeft.isLeft = false;
        CheckRight.isRight = false;
    }

    
    private void Reaparecer()
    {
        if (tiempoCaer > 0.3) {
            GetComponent<Rigidbody2D>().drag = 100f;
        }
        else if (tiempoCaer > 0.2)
        {
            GetComponent<Rigidbody2D>().drag = 300f;
        }
        else if (tiempoCaer > 0)
        {
            GetComponent<Rigidbody2D>().drag = 3000f;
        }
        else 
        {
            reapareciendo = false;
            reaparecer = false; 
            cayendo = true;
            GetComponent<Rigidbody2D>().drag = 0f;
        }
    }
    
    //Sockets
    private void ThreadMethod()
    {
        //Obtengo la dirección IP de este pc
        string ipPC = string.Empty;
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addr = ipEntry.AddressList;
        ipPC = addr[1].ToString();


        Debug.Log(ipPC);
        Debug.Log("VOY A EMPEZAR A RECIBIR MENSAJES");
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(ipPC), 0);
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

            if (cadena.Contains("Jump")){
                saltar = true;
            }

        }
    }
}
