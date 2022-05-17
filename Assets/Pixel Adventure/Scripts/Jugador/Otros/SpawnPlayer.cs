using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject playerFrefap;
    //public Vector3 posicionInicial = new Vector3(0, 1);
    
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerFrefap,transform.position,new Quaternion());
    }

    public void Spawn()
    {
        if (PlayerPrefs.GetInt("Vidas") >= 0)
        {
            Instantiate(playerFrefap,transform.position,new Quaternion());
        } else {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
