using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Partida : MonoBehaviour
{
    public static int vidas;

    private bool terminado = false;
    private bool unaVez = true;
    private double lluvia = 2.3f;
    // en segundos
    public static int tiempo = 120;
    // Start is called before the first frame update
    void Start()
    {
        vidas = Inicio.vidas;
    }

    private void Update()
    {
        if (terminado)
        {
            NivelSuperado();
        }
    }

    public void NivelSuperado()
    {
        terminado = true;
        lluvia = lluvia - Time.deltaTime;
        if (unaVez)
        {
            unaVez = false;
            PlayerPrefs.SetInt("Vidas", PlayerPrefs.GetInt("Vidas") + 3);
            GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMove>().PasarNivel();


            if (GameObject.FindGameObjectWithTag("Player2"))
            {
                GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMove2>().PasarNivel();
            }

            GameObject.Find("SpawnLluvia").GetComponent<SpawnLluvia>().Spawn();
            if (GameObject.Find("SpawnLluvia2"))
            {
                GameObject.Find("SpawnLluvia2").GetComponent<SpawnLluvia>().Spawn();
            }
        }

        if (lluvia <= 0) 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void TiempoAgotado()
    {
        if(GameObject.FindGameObjectWithTag("Player1"))
        {
            if (!GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMove>().dead)
            {
                GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerMove>().Morir();
            }
            
            if (GameObject.FindGameObjectWithTag("Player2"))
            {
                if (!GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMove2>().dead)
                {
                    GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerMove2>().Morir();
                }
            }
            
        }
    }
}
