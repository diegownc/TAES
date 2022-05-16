using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public static int vidas = 5;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Vidas",vidas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
