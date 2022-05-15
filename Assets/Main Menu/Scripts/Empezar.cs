using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Empezar : MonoBehaviour
{
    public Text textElement;
    
    void Empezar_Juego()
    {
        StartCoroutine(DelayEmpezar_Juego());
    }

    public IEnumerator DelayEmpezar_Juego()
    {
        textElement.text = PlayerPrefs.GetString("nombreDelJuego");
        yield return new WaitForSeconds(.5f);

        if (textElement.text == "Pixel Adventure")
        {
            SceneManager.LoadScene("Level_1");
        }
        else if (textElement.text == "Extreme Football")
        {
            SceneManager.LoadScene("Extreme Football");
        }
        else if (textElement.text == "Rhythm Run")
        {
            SceneManager.LoadScene("Rhythm Run");
        }
    }
}
