using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnJumpEffect : MonoBehaviour
{
    public GameObject jumpEffect;
    public GameObject Humo;

    public void Spawn(Vector2 posicion)
    {
        Humo = Instantiate(jumpEffect,new Vector2(posicion.x,posicion.y),new Quaternion());
        Destroy(Humo,0.3f);
    }
}
