using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardarNombreDeJuego : MonoBehaviour
{
    public void SetText(string text) {
        PlayerPrefs.SetString("nombreDelJuego", text);
        Console.WriteLine("SU");
    }
}
