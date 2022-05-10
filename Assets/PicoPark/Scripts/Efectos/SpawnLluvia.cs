using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLluvia : MonoBehaviour
{
    public GameObject LluviaFrutas;
    public GameObject instancia;

    public void Spawn()
    {
        instancia = Instantiate(LluviaFrutas);
        Destroy(instancia,5f);
    }
}
