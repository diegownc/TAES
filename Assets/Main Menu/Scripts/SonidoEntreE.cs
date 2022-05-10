using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonidoEntreE : MonoBehaviour
{
    private SonidoEntreE instance;
    public SonidoEntreE Instance{
        get{
            return instance;
        }
    }

    private void Awake() 
    {
        if(FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void quitarMusica()
    {
        Destroy(gameObject);
    }
}
