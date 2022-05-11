using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Net;
using System;
using System.Net.Sockets;

public class CambiarEscena : MonoBehaviour
{
    void Start()
    {
        string localIP = string.Empty;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        Debug.Log(localIP);
    }
    void LoadScene(string sceneName)
    {
        StartCoroutine(Delay_LoadScene(sceneName));
    }

    public IEnumerator Delay_LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(sceneName);
    }
}
