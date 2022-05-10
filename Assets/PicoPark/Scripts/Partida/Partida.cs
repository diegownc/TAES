using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Partida : MonoBehaviour
{
    public static int vidas = 5;
    // en segundos
    public static int tiempo = 20;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Vidas",vidas);
    }

    public void NivelSuperado()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().PasarNivel();
        GameObject.Find("SpawnLluvia").GetComponent<SpawnLluvia>().Spawn();
    }
    
    public void TiempoAgotado()
    {
        if(!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().dead)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().dead = true;
            GameObject.Find("SpawnLluvia").GetComponent<SpawnLluvia>().Spawn();
        }
    }
}
