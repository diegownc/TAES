using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
