using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIWinLose : MonoBehaviour
{

	[SerializeField] TextMeshProUGUI winLoseText = null;

	int playerPlanetsCount = 0,
		totalPlanetsCount = 0;



	void Start()
	{
		totalPlanetsCount = Planet.Planets.Count;
		winLoseText.text = "";
	}



	void Update()
    {
		playerPlanetsCount = Planet.GetCountOfMyPlanets(1);

		if (playerPlanetsCount == totalPlanetsCount)
		{
			winLoseText.text = "Victory!\nAll Planets Have Been Conquered";
			return;
		}

		if (playerPlanetsCount == 0)
		{
			winLoseText.text = "Defeat!\nAll Planets Have Been Lost";
			return;
		}

		winLoseText.text = "";
	}
}
