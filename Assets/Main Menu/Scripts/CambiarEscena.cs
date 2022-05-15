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
        
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(Delay_LoadScene(sceneName));
    }

    public IEnumerator Delay_LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(sceneName);
    }

    public void Salir()
    {
        Application.Quit();
    }
}
