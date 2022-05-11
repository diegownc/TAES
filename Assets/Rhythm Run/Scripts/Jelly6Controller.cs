using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using System;

public class Jelly6Controller : MonoBehaviour
{
    public FondoController fondo;
    public NotaController nota;
    public Marca6Controller marca;
    public Sprite malSprite;
    public Sprite okSprite;
    public Sprite goodSprite;
    public Sprite yeahSprite;
    public Sprite perfectSprite;
    public ParticulasController particulas;

    public Jelly2Controller player2;
    public Jelly3Controller player3;
    public Jelly4Controller player4;
    public Jelly5Controller player5;
    public Jelly1Controller player1;

    static UdpClient udp;
    Thread thread;
    private bool push = false;


    public int veces = 0;
    public int pulsos = 0;
    public bool fin = true;


    // Start is called before the first frame update
    void Start()
    {

        nota = FindObjectOfType<NotaController>();
        marca = FindObjectOfType<Marca6Controller>();

        player2 = FindObjectOfType<Jelly2Controller>();
        player3 = FindObjectOfType<Jelly3Controller>();
        player4 = FindObjectOfType<Jelly4Controller>();
        player5 = FindObjectOfType<Jelly5Controller>();
        player1 = FindObjectOfType<Jelly1Controller>();


        marca.gameObject.SetActive(false);

        if (fondo != null)
        {
            fondo.gameObject.SetActive(false);
        }

        particulas.gameObject.SetActive(false);

        gameObject.GetComponent<Transform>().position = new Vector3(-480, -330, 0);

        udp = new UdpClient(8061);
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
        if (gameObject.transform.position.x < 550)
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
                if ((Input.GetKey(KeyCode.H) || push) && veces == 0)
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
            if (fin)
            {
                nota.gameObject.SetActive(false);
                marca.gameObject.SetActive(false);

                nota.parar();
                fondo.ActivateSprite();
                particulas.gameObject.SetActive(true);
                gameObject.GetComponent<Transform>().position = new Vector3(0, 120, 0);
                //2 -340 -80
                //3 160 -9
                float pos2 = player2.transform.position.x;
                int segundo = 2;
                if (player3.transform.position.x > pos2)
                {
                    pos2 = player3.transform.position.x;
                    segundo = 3;
                }
                if (player4.transform.position.x > pos2)
                {
                    pos2 = player4.transform.position.x;
                    segundo = 4;
                }
                if (player5.transform.position.x > pos2)
                {
                    pos2 = player5.transform.position.x;
                    segundo = 5;
                }
                if (player1.transform.position.x > pos2)
                {
                    pos2 = player1.transform.position.x;
                    segundo = 1;
                }



                float pos3 = -999;
                int tercero = 0;

                if (segundo == 2)
                {
                    pos3 = player3.transform.position.x;
                    tercero = 3;
                    if (player4.transform.position.x > pos3)
                    {
                        pos3 = player4.transform.position.x;
                        tercero = 4;
                    }
                    if (player5.transform.position.x > pos3)
                    {
                        pos3 = player5.transform.position.x;
                        tercero = 5;
                    }
                    if (player1.transform.position.x > pos3)
                    {
                        pos3 = player1.transform.position.x;
                        tercero = 1;
                    }
                }
                if (segundo == 3)
                {
                    pos3 = player2.transform.position.x;
                    tercero = 2;
                    if (player4.transform.position.x > pos3)
                    {
                        pos3 = player4.transform.position.x;
                        tercero = 4;
                    }
                    if (player5.transform.position.x > pos3)
                    {
                        pos3 = player5.transform.position.x;
                        tercero = 5;
                    }
                    if (player1.transform.position.x > pos3)
                    {
                        pos3 = player1.transform.position.x;
                        tercero = 1;
                    }
                }
                if (segundo == 4)
                {
                    pos3 = player2.transform.position.x;
                    tercero = 2;
                    if (player3.transform.position.x > pos3)
                    {
                        pos3 = player3.transform.position.x;
                        tercero = 3;
                    }
                    if (player5.transform.position.x > pos3)
                    {
                        pos3 = player5.transform.position.x;
                        tercero = 5;
                    }
                    if (player1.transform.position.x > pos3)
                    {
                        pos3 = player1.transform.position.x;
                        tercero = 1;
                    }
                }
                if (segundo == 5)
                {
                    pos3 = player2.transform.position.x;
                    tercero = 2;
                    if (player3.transform.position.x > pos3)
                    {
                        pos3 = player3.transform.position.x;
                        tercero = 3;
                    }
                    if (player4.transform.position.x > pos3)
                    {
                        pos3 = player4.transform.position.x;
                        tercero = 4;
                    }
                    if (player1.transform.position.x > pos3)
                    {
                        pos3 = player1.transform.position.x;
                        tercero = 1;
                    }
                }
                if (segundo == 1)
                {
                    pos3 = player2.transform.position.x;
                    tercero = 2;
                    if (player3.transform.position.x > pos3)
                    {
                        pos3 = player3.transform.position.x;
                        tercero = 3;
                    }
                    if (player4.transform.position.x > pos3)
                    {
                        pos3 = player4.transform.position.x;
                        tercero = 4;
                    }
                    if (player5.transform.position.x > pos3)
                    {
                        pos3 = player5.transform.position.x;
                        tercero = 5;
                    }
                }


                player2.gameObject.GetComponent<Transform>().position = new Vector3(-250, -300, 0);
                player3.gameObject.GetComponent<Transform>().position = new Vector3(-150, -290, 0);
                player4.gameObject.GetComponent<Transform>().position = new Vector3(-70, -310, 0);
                player5.gameObject.GetComponent<Transform>().position = new Vector3(50, -300, 0);
                player1.gameObject.GetComponent<Transform>().position = new Vector3(-350, -300, 0);

                if (segundo == 2)
                {
                    player2.gameObject.GetComponent<Transform>().position = new Vector3(-380, -40, 0);
                }
                if (segundo == 3)
                {
                    player3.gameObject.GetComponent<Transform>().position = new Vector3(-380, -40, 0);
                }
                if (segundo == 4)
                {
                    player4.gameObject.GetComponent<Transform>().position = new Vector3(-380, -40, 0);
                }
                if (segundo == 5)
                {
                    player5.gameObject.GetComponent<Transform>().position = new Vector3(-380, -40, 0);
                }
                if (segundo == 1)
                {
                    player1.gameObject.GetComponent<Transform>().position = new Vector3(-380, -40, 0);
                }
                if (tercero == 2)
                {
                    player2.gameObject.GetComponent<Transform>().position = new Vector3(340, -20, 0);
                }
                if (tercero == 3)
                {
                    player3.gameObject.GetComponent<Transform>().position = new Vector3(340, -20, 0);
                }
                if (tercero == 4)
                {
                    player4.gameObject.GetComponent<Transform>().position = new Vector3(340, -20, 0);
                }
                if (tercero == 5)
                {
                    player5.gameObject.GetComponent<Transform>().position = new Vector3(340, -20, 0);

                }
                if (tercero == 1)
                {
                    player1.gameObject.GetComponent<Transform>().position = new Vector3(340, -20, 0);
                }
                fin = false;
            }
        }
    }
}
