using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXMenuCameraShake : MonoBehaviour
{

	Vector3 baseForward;
	float reducer = 18,
		speed = .2f;



	void Start()
	{
		baseForward = transform.forward;
	}



	void Update()
	{
		var value = Time.time * speed;
		transform.rotation = Quaternion.LookRotation(baseForward * reducer + 
			new Vector3(Mathf.PerlinNoise(value, value) - 0.5f, Mathf.PerlinNoise(value * 0.5f, value * 0.5f) - 0.5f, 0), Vector3.up);
	}
}
