using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuSettings : MonoBehaviour
{

	[SerializeField] Text minPlanetsText = null;
	[SerializeField] Text maxPlanetsText = null;
	[SerializeField] Text playerCountText = null;
	[SerializeField] Text mapSizeText = null;



	void Start()
	{
		minPlanetsText.text = MapGenerator.MinPlanets.ToString();
		maxPlanetsText.text = MapGenerator.MaxPlanets.ToString();
		playerCountText.text = MapGenerator.FactionCount.ToString();
		mapSizeText.text = MapGenerator.MapRadius.ToString();
	}






	public void DecreaseMinPlanets()
	{
		MapGenerator.MinPlanets--;
		minPlanetsText.text = MapGenerator.MinPlanets.ToString();
	}



	public void IncreaseMinPlanets()
	{
		MapGenerator.MinPlanets++;
		minPlanetsText.text = MapGenerator.MinPlanets.ToString();
	}






	public void DecreaseMaxPlanets()
	{
		MapGenerator.MaxPlanets--;
		maxPlanetsText.text = MapGenerator.MaxPlanets.ToString();
	}



	public void IncreaseMaxPlanets()
	{
		MapGenerator.MaxPlanets++;
		maxPlanetsText.text = MapGenerator.MaxPlanets.ToString();
	}






	public void DecreasePlayers()
	{
		MapGenerator.FactionCount--;
		playerCountText.text = MapGenerator.FactionCount.ToString();
	}



	public void IncreasePlayers()
	{
		MapGenerator.FactionCount++;
		playerCountText.text = MapGenerator.FactionCount.ToString();
	}






	public void DecreaseMapSize()
	{
		MapGenerator.MapRadius--;
		mapSizeText.text = MapGenerator.MapRadius.ToString();
	}



	public void IncreaseMapSize()
	{
		MapGenerator.MapRadius++;
		mapSizeText.text = MapGenerator.MapRadius.ToString();
	}
}
