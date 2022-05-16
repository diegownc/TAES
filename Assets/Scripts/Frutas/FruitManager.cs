using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitManager : MonoBehaviour
{
    private bool unaVez = true;
    public void Update()
    {
        AllFruitCollected();
    }

    public void AllFruitCollected()
    {
        if (transform.childCount == 0 && unaVez)
        {
            unaVez = false;
            GameObject.Find("Partida").GetComponent<Partida>().NivelSuperado();
        }
    }
}
