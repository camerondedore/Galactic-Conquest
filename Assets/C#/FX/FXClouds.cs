using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXClouds : MonoBehaviour
{

	float speed = 0;



	void Start()
	{
		speed = 2 * (Random.Range(0, 2) * 2 - 1);
		GetComponent<Renderer>().material.mainTextureScale = Vector2.one * Random.Range(1, 3);
	}



	void Update()
    {
		transform.Rotate(transform.up, speed * Time.deltaTime);
    }
}
