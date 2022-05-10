using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CambiarEscena : MonoBehaviour
{
    void LoadScene(string sceneName)
    {
        StartCoroutine(Delay_LoadScene(sceneName));
    }

    public IEnumerator Delay_LoadScene(string sceneName)
    {
        yield return new WaitForSeconds(.3f);
        SceneManager.LoadScene(sceneName);
    }
}
