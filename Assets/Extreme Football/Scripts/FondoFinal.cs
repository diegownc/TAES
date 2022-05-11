using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoFinal : MonoBehaviour
{
    public SpriteRenderer spriteredererFondo;

    void Start()
    {
        spriteredererFondo = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (TimeController.finPartido)
        {
            spriteredererFondo.sortingOrder = -3;
        }
    }
}
