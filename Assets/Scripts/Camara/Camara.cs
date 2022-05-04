using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour
{

    public Vector3 offset = new Vector3(0, 0, -10);
    public float rapidez = 0.0f;
    
    private Vector3 vel;
    private GameObject personaje;
    private Vector3 position;
    void Start()
    {
        if (personaje == null)
        {
            if (GameObject.FindWithTag("Player"))
                personaje = GameObject.FindWithTag("Player");
            Debug.Log("No hay player");
        }
        else
        {
            position = personaje.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, position, ref vel, rapidez);
            Debug.Log("Hay personaje");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (personaje == null)
        {
            if (GameObject.FindWithTag("Player"))
                personaje = GameObject.FindWithTag("Player");
            Debug.Log("No hay player");
        }
        else
        {
            position = personaje.transform.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, position, ref vel, rapidez);
            Debug.Log("Hay personaje");
        }
    }
}
