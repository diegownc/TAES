using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitarMusicaMenu : MonoBehaviour
{
    private GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.FindGameObjectWithTag("Control Audio");

        if(obj != null)
        {
            obj.GetComponent<SonidoEntreE>().quitarMusica();
        }
    }
}
