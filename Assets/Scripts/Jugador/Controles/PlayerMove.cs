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
    public bool telefono = false;
    Thread thread;
    //Movimientos
    private bool moverDerecha = false;
    private bool moverIzquierda = false;
    private bool saltar = false;
    private bool dobleSalto = false;
    
    //Controles
    private float horizontalInput;

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
    //Estado nivel
    private bool pasarNivel = false;
    //Jugabilidad
    private bool segundoSaltoDisponible = false;

    //Atributos de metodos
    private double frameSalto = 0;
    private double frameReaparecer = 0;
    private float tiempoCaer = 0.95f;
    private float tiempoReaparecer = 0.4f;
    private bool restarVida = true;

    private void Start()
    {
        frameReaparecer = transform.position.y;

        if (telefono)
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
            ResetCheck();
            AnimacionEnAire();
            tiempoCaer = tiempoCaer - Time.deltaTime;
            Reaparecer();
        }
        else if (dead)
        {
            ResetCheck();
            Morir();
        }
        else if (muriendo)
        {
            if (!pasarNivel)
            {
                tiempoReaparecer = tiempoReaparecer - Time.deltaTime;
                PlayerSpawn();
            }
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
        //Reaparicion
        if (reaparecer && frameReaparecer > transform.position.y + 2)
        {
            reaparecer = false;
            cayendo = true;
        }

        if (!dobleSalto)
        {
            if (gameObject.GetComponent<Rigidbody2D>().velocity.y > 0.2)
            {
                saltando = true;
                cayendo = false;
                suelo = false;
            }
            else if (gameObject.GetComponent<Rigidbody2D>().velocity.y < -0.2)
            {
                saltando = false;
                cayendo = true;
                suelo = false;
            }
            if(CheckGround.isGround){
                dobleSalto = false;
                saltando = false;
                cayendo = false;
                suelo = true;
            }
        }
        else
        {
            if(CheckGround.isGround){
                dobleSalto = false;
                saltando = false;
                cayendo = false;
                suelo = true;
            }
        }
    }

    private void ComportamiendoEnAire()
    {
        if (!suelo && !dobleSalto)
        {
            //efecto visual chido cuando salta
            if (saltando && gameObject.transform.position.y > 0.5 + frameSalto)
            {
                saltando = false;
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

        if (dobleSalto)
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", false);
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
            gameObject.GetComponent<Animator>().SetBool("DobleSalto", true);
        }
        else if (suelo)
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", false);
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
            gameObject.GetComponent<Animator>().SetBool("DobleSalto", false);
        }
        else if (cayendo)
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", true);
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
        }
        else if (saltando)
        {
            gameObject.GetComponent<Animator>().SetBool("Fall", false);
            gameObject.GetComponent<Animator>().SetBool("Jump", true);
        }
        else {
            //Por si buguea
            gameObject.GetComponent<Animator>().SetBool("Fall", false);
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
            gameObject.GetComponent<Animator>().SetBool("DobleSalto", false);
        }
    }

    private void MovimientoHorizontal()
    {
        if (!telefono)
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
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Run", false);
            
            if(suelo && !saltando && !cayendo && !reapareciendo && !dobleSalto)
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void Salto()
    {
        if (telefono)
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
                    cayendo = false;
                    segundoSaltoDisponible = false;
                    saltando = false;
                    GameObject.Find("SpawnJumpEffect").GetComponent<SpawnJumpEffect>().Spawn(transform.position);
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, elevacionDobleSalto);
                    frameSalto = gameObject.transform.position.y;
                }
            }
        }
        else
        {
            if (Input.GetKeyDown("w")){
                if (suelo)
                {
                    saltando = true;
                    frameSalto = gameObject.transform.position.y;
                    gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, fuerzaSaltoSimple));
                    segundoSaltoDisponible = true;
                }
                else if (segundoSaltoDisponible)
                {
                    gameObject.GetComponent<Animator>().SetBool("DobleSalto", true);
                    dobleSalto = true;
                    saltando = false;
                    cayendo = false;
                    segundoSaltoDisponible = false;
                    GameObject.Find("SpawnJumpEffect").GetComponent<SpawnJumpEffect>().Spawn(transform.position);
                    gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, elevacionDobleSalto);
                    frameSalto = gameObject.transform.position.y;
                }
            }
        }
    }

    private void Morir()
    {
        //Para que se reste solo una vida
        if (!muriendo)
        {
            PlayerPrefs.SetInt("Vidas", PlayerPrefs.GetInt("Vidas") - 1);
        }

        dead = false;
        muriendo = true;
        gameObject.GetComponent<Animator>().SetBool("Reaparecer", false);
        gameObject.GetComponent<Animator>().SetBool("Run", false);
        gameObject.GetComponent<Animator>().SetBool("Fall", false);
        gameObject.GetComponent<Animator>().SetBool("Jump", false);
        gameObject.GetComponent<Animator>().SetBool("DobleSalto", false);
        gameObject.GetComponent<Animator>().SetBool("Morrir", true);
        ResetCheck();
        if(telefono)
            udp.Close();
        Destroy(gameObject, 0.5f);
        Quieto();
    }

    private void Quieto()
    {
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void PasarNivel()
    {
        pasarNivel = true;
        dead = false;
        muriendo = true;
        gameObject.GetComponent<Animator>().SetBool("Reaparecer", false);
        gameObject.GetComponent<Animator>().SetBool("Run", false);
        gameObject.GetComponent<Animator>().SetBool("Fall", false);
        gameObject.GetComponent<Animator>().SetBool("Jump", false);
        gameObject.GetComponent<Animator>().SetBool("DobleSalto", false);
        gameObject.GetComponent<Animator>().SetBool("Morrir", true);
        ResetCheck();
        if(telefono)
            udp.Close();
        Destroy(gameObject, 0.5f);
        Quieto();
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
