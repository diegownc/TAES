using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContadorVidas : MonoBehaviour
{
    public TextMeshProUGUI vidas;
    private int contador;
    // Start is called before the first frame update
    void Start()
    {
        contador = PlayerPrefs.GetInt("Vidas");
    }

    // Update is called once per frame
    void Update()
    {
        contador = PlayerPrefs.GetInt("Vidas");
        vidas.text = "" + contador;
    }
}
