using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System;

public class Player4Controller : MonoBehaviour
{
    public FondoController fondo;
    public NotaController nota;
    public Marca4Controller marca;
    public Sprite malSprite;
    public Sprite okSprite;
    public Sprite goodSprite;
    public Sprite yeahSprite;
    public Sprite perfectSprite;

    public Player2Controller player2;
    public Player3Controller player3;
    public Player1Controller player1;
    public Player5Controller player5;
    public Player6Controller player6;

    static UdpClient udp;
    Thread thread;
    private bool push = false;


    public int veces = 0;
    public int pulsos = 0;


    // Start is called before the first frame update
    void Start()
    {
        fondo = FindObjectOfType<FondoController>();
        nota = FindObjectOfType<NotaController>();
        marca = FindObjectOfType<Marca4Controller>();

        player2 = FindObjectOfType<Player2Controller>();
        player3 = FindObjectOfType<Player3Controller>();
        player1 = FindObjectOfType<Player1Controller>();
        player5 = FindObjectOfType<Player5Controller>();
        player6 = FindObjectOfType<Player6Controller>();


        marca.gameObject.SetActive(false);

        if (fondo != null)
        {
            fondo.gameObject.SetActive(false);
        }

        gameObject.GetComponent<Transform>().position = new Vector3(-480, -150, 0);

        udp = new UdpClient(8059);
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start();
        push = false;

    }

    private void ThreadMethod()
    {
        //Obtengo la dirección IP de este pc
        string ipPC = string.Empty;
        IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress[] addr = ipEntry.AddressList;
        //ipPC = addr[1].ToString();
        ipPC = "192.168.170.32";


        Debug.Log(ipPC);
        Debug.Log("VOY A EMPEZAR A RECIBIR MENSAJES");
        while (true)
        {
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Parse(ipPC), 0);
            byte[] receiveBytes = udp.Receive(ref RemoteIpEndPoint);
            string cadena = Encoding.UTF8.GetString(receiveBytes);

            if (cadena.Contains("Push"))
            {
                push = true;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x < 750)
        {

            if (pulsos > 0)
            {
                pulsos--;
            }
            if (pulsos == 0)
            {
                marca.gameObject.SetActive(false);
            }
            if (nota.transform.position.x < -800)
            {
                veces = 0;
            }
            else
            {
                if ((Input.GetKey(KeyCode.F) || push) && veces == 0)
                {
                    push = false;
                    if (nota.transform.position.x < 5 && nota.transform.position.x > -5)
                    {
                        marca.ChangeSprite(perfectSprite);
                        gameObject.transform.Translate(200, 0, 0);
                        veces = 1;
                        pulsos = 200;
                        marca.gameObject.SetActive(true);

                    }
                    if ((nota.transform.position.x < -5 && nota.transform.position.x > -15) || (nota.transform.position.x < 15 && nota.transform.position.x > 5))
                    {
                        marca.ChangeSprite(yeahSprite);
                        gameObject.transform.Translate(150, 0, 0);
                        veces = 1;
                        pulsos = 200;
                        marca.gameObject.SetActive(true);
                    }
                    if ((nota.transform.position.x < -15 && nota.transform.position.x > -25) || (nota.transform.position.x < 25 && nota.transform.position.x > 15))
                    {
                        marca.ChangeSprite(goodSprite);
                        gameObject.transform.Translate(100, 0, 0);
                        veces = 1;
                        pulsos = 200;
                        marca.gameObject.SetActive(true);
                    }
                    if ((nota.transform.position.x < -25) || (nota.transform.position.x > 25))
                    {
                        marca.ChangeSprite(malSprite);
                        veces = 1;
                        pulsos = 200;
                        marca.gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            nota.gameObject.SetActive(false);
            marca.gameObject.SetActive(false);

            nota.parar();
            fondo.ActivateSprite();
            gameObject.GetComponent<Transform>().position = new Vector3(0, 120, 0);
            player2.gameObject.GetComponent<Transform>().position = new Vector3(-250, -350, 0);
            player3.gameObject.GetComponent<Transform>().position = new Vector3(-150, -350, 0);
            player1.gameObject.GetComponent<Transform>().position = new Vector3(-350, -350, 0);
            player5.gameObject.GetComponent<Transform>().position = new Vector3(50, -350, 0);
            player6.gameObject.GetComponent<Transform>().position = new Vector3(150, -350, 0);

        }
    }
}
