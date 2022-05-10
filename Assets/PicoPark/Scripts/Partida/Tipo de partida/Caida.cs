using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caida : MonoBehaviour
{
    private GameObject personaje;
    private GameObject camara;
    public GameObject paracaidas;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("Player"))
        {
            personaje = GameObject.FindWithTag("Player");
            camara = GameObject.FindWithTag("MainCamera");
        }
        else Debug.Log("No hay player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!personaje)
        {
            paracaidas.GetComponent<SpriteRenderer>().enabled = false;
            personaje = GameObject.FindWithTag("Player");
            camara = GameObject.FindWithTag("MainCamera");
        }
        else if (personaje)
        {
            if (personaje.transform.position.y < -1.4f)
            {
                if(paracaidas.GetComponent<SpriteRenderer>().enabled)
                    paracaidas.transform.position = personaje.transform.position;
                
                personaje.GetComponent<PlayerMove>().velocidad = 4;
                camara.GetComponent<Camara>().offset.y = -2.3f;
                camara.GetComponent<Camera>().orthographicSize = 3.5f;
                if (personaje.GetComponent<Rigidbody2D>().velocity.y < -10)
                {
                    paracaidas.GetComponent<SpriteRenderer>().enabled = true;
                    
                    personaje.GetComponent<Rigidbody2D>().drag = 2f;
                }
            }
        }
    }
}
