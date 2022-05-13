using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class FondoFinal : MonoBehaviour
{
    public SpriteRenderer spriteredererFondo;
    public Text Winer;

    void Start()
    {
        spriteredererFondo = GetComponent<SpriteRenderer>();
        Winer.enabled = false;
    }

    void Update()
    {
        if (TimeController.finPartido)
        {
            spriteredererFondo.sortingOrder = 0;
            Winer.enabled = true;

            if (GoalPlayer1.score1 > GoalPlayer2.score2)
            {
                //Gana Jugador2 (nombre jugador)
                Winer.text = "GANADOR " + "Nombre P2";
            }
            else if (GoalPlayer1.score1 < GoalPlayer2.score2)
            {
                //Gana Jugador1 (nombre jugador)
                Winer.text = "GANADOR " + "Nombre P1";
            }
            else
            {
                //Empate
                Winer.text = "EMPATE";
            }
        }
    }
}
