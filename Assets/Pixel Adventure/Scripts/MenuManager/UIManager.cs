using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject panelOptions;

    public void PanelOptions()
    {
        Time.timeScale = 0;
        panelOptions.SetActive(true);
    }

    public void Return()
    {
        Time.timeScale = 1;
        panelOptions.SetActive(false);
    }


    public void GoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
