using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Net;

public class PonerTituloDelJuego : MonoBehaviour
{
    public Text textElement;
    public Text IP;

    // Start is called before the first frame update
    void Start()
    {
        textElement.text = PlayerPrefs.GetString("nombreDelJuego");
        string ipPC = string.Empty;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            ipPC = endPoint.Address.ToString();
        }
        IP.text = ipPC;
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
