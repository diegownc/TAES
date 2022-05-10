using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContadorTiempo : MonoBehaviour
{
    public TextMeshProUGUI tiempo;
    private float contador;
    private int minutos;
    private int segundos;
    private bool unaVez = true;
    
    // Start is called before the first frame update
    void Start()
    {
        contador = Partida.tiempo;
        minutos = (int) contador / 60;
        segundos = (int) contador % 60;
        tiempo.text = " " + minutos; //+ ": " + segundos;

    }

    // Update is called once per frame
    void Update()
    {
        if (contador < 0)
        {
            tiempo.text = "Fin de partida puta";
            if (unaVez)
            {
                unaVez = false;
                GameObject.Find("Partida").GetComponent<Partida>().TiempoAgotado();
            }
        }
        else
        {
            minutos = (int) contador / 60;
            segundos = (int) contador % 60;
            if(segundos < 10) 
                tiempo.text = " " + minutos + ":0" + segundos;
            else tiempo.text = " " + minutos + ":" + segundos;
        }
        contador -= Time.deltaTime;
    }
}
