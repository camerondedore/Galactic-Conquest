using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{

	bool paused = false;
	GameObject menu = null;



	void Start()
	{
		menu = GameObject.FindGameObjectWithTag("Menu");
		Time.timeScale = 1;
		Time.fixedDeltaTime = 0.02f;
		menu.SetActive(false);
	}



	void Update()
	{
		PauseCheck();
	}



	void PauseCheck()
	{
		if (Input.GetButtonDown("Pause"))
		{
			paused = !paused;

			if (!paused)
			{
				Time.timeScale = 1;
				Time.fixedDeltaTime = 0.02f;
				menu.SetActive(false);
			}
			else
			{
				Time.timeScale = 0;
				menu.SetActive(true);
			}
		}
	}
}
