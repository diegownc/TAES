using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AjustesManager : MonoBehaviour
{

    public GameObject panelOptions;
    public AudioSource AudioMenu;
    public AudioSource AudioPartido;
    public AudioSource AudioFinPartido;

    public void PanelOptions()
    {
        Time.timeScale = 0;
        panelOptions.SetActive(true);

        if (!TimeController.finPartido)
        {
            AudioPartido.Pause();
        }
        else if (TimeController.finPartido)
        {
            AudioFinPartido.Pause();
        }
    }

    public void Voler()
    {
        Time.timeScale = 1;
        panelOptions.SetActive(false);

        if (!TimeController.finPartido)
        {
            AudioPartido.Play();
        }
        else if (TimeController.finPartido)
        {
            AudioFinPartido.Play();
        }
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

        //Si no va el stop, poner mute
        if (!TimeController.finPartido)
        {
            AudioPartido.mute = true;
            AudioPartido.Stop();

        }
        else if (TimeController.finPartido)
        {
            AudioFinPartido.mute = true;
            AudioFinPartido.Stop();
        }
    }

    public void Update()
    {
        if (TimeController.finPartido){
            //AudioPartido.Stop();
            AudioPartido.mute = true;
            //AudioFinPartido.Play();
            AudioFinPartido.mute = false;
        }
    }

    /**public void SalirdelJuego()
    {
        Application.Quit();
    }**/

    public void PlaySound()
    {
        AudioMenu.Play();
    }
}
