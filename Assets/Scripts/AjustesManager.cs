using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AjustesManager : MonoBehaviour
{

    public GameObject panelOptions;

    public void PanelOptions()
    {
        Time.timeScale = 0;
        panelOptions.SetActive(true);
    }

    public void Voler()
    {
        Time.timeScale = 1;
        panelOptions.SetActive(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    /**public void SalirdelJuego()
    {
        Application.Quit();
    }**/
}
