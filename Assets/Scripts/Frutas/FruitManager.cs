using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitManager : MonoBehaviour
{
    public void Update()
    {
        AllFruitCollected();
    }

    public void AllFruitCollected()
    {
        if (transform.childCount == 0)
        {
            Debug.Log("No quedan frutas, Victoria!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
