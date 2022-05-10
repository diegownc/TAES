using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Empezar : MonoBehaviour
{
    public Text textElement;
    public void Empezar_Juego()
    {
        textElement.text = PlayerPrefs.GetString("nombreDelJuego");

        if (textElement.text == "Juego1")
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (textElement.text == "Extreme Football")
        {
            SceneManager.LoadScene("ExtremeFootball");
        }
        else if (textElement.text == "Rythm run")
        {
            SceneManager.LoadScene("RythmRun");
        }
    }
}
