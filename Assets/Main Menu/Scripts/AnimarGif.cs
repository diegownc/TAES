using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System;

public class AnimarGif : MonoBehaviour
{
	public Sprite[] frames;
	public Image animatedObject;
	public int fps = 10;
	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		int index = (int)(Time.time * fps) % frames.Length;
		animatedObject.sprite = frames[index];
	}
}