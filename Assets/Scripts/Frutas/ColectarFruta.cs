using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ColectarFruta : MonoBehaviour
{
    public AudioSource clip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);

            FindObjectOfType<FruitManager>().AllFruitCollected();

            Destroy(gameObject,0.5f);

            clip.Play();
        }
    }
}
