using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PonerTituloDelJuego : MonoBehaviour
{
    public Text textElement;

    // Start is called before the first frame update
    void Start()
    {
        textElement.text = PlayerPrefs.GetString("nombreDelJuego");
    }

    // Update is called once per frame
    void Update()
    {
  
    }
}
