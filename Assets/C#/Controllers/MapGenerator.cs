using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapGenerator : MonoBehaviour
{
    #region Fields
    public static int planetCount = 0;
    static float mapRadius = 20;
    static int factionCount = 2,
        minPlanets = 10,
        maxPlanets = 15;

    [SerializeField] GameObject planet = null,
        sun = null;
    [SerializeField] float mapHeight = 1;
    #endregion

    #region Properties
	public static int FactionCount
	{
		get
		{
			return factionCount;
		}

		set
		{
			factionCount = Mathf.Clamp(value, 2, 4);
		}
	}

	public static int MinPlanets
	{
		get
		{
			return minPlanets;
		}

		set
		{
			minPlanets = Mathf.Clamp( value, 1, maxPlanets);
		}
	}

	public static int MaxPlanets
	{
		get
		{
			return maxPlanets;
		}

		set
		{
			maxPlanets = Mathf.Clamp( value, minPlanets, 30);
		}
	}

	public static float MapRadius
	{
		get
		{
			return mapRadius;
		}

		set
		{
			mapRadius = Mathf.Clamp(value, 20, 60);
		}
	}
	#endregion



	#region Methods
	void Start()
    {
        SpawnPlayerPlanets();
        SpawnGaiaPlanets();
        SpawnSun();
    }



    void SpawnPlayerPlanets()
    {
        int playerNumber = 1;
        planetCount += factionCount;
        Vector3 spawnPos = Vector3.zero;
        float playerAngle = 6.28f / factionCount;
        float phaseShift = Random.Range(0f, 6.28f);

        while (playerNumber <= factionCount)
        {
            var angle = playerAngle * playerNumber + phaseShift;
            spawnPos = new Vector3(Mathf.Cos(angle) * mapRadius, Random.Range(-mapHeight, mapHeight), Mathf.Sin(angle) * mapRadius);

            CreatePlanet(playerNumber, spawnPos);

            playerNumber++;
        }
    }



    void SpawnGaiaPlanets()
    {
        int count = Random.Range(minPlanets, maxPlanets + 1);
        planetCount += count;

        while (count > 0)
        {
            Vector3 spawnPos = Vector3.zero;

            var validPos = false;

            while (!validPos)
            {
                var angle = Random.Range(0f, 6.28f);
                var myRadius = Random.Range(0f, mapRadius);
                spawnPos = new Vector3(Mathf.Cos(angle) * myRadius, Random.Range(-mapHeight, mapHeight), Mathf.Sin(angle) * myRadius);

                validPos = IsFarEnoughAway(spawnPos);
            }

            CreatePlanet(0, spawnPos);

            count--;
        }
    }



    void SpawnSun()
    {
        var spawnPos = Vector3.zero;
        bool validPos = false;

        while (!validPos)
        {
            spawnPos = new Vector3(Mathf.Round(Random.Range(-mapRadius, mapRadius)),
                Random.Range(-mapHeight, mapHeight),
                Mathf.Round(Random.Range(-mapRadius, mapRadius)));

            validPos = IsFarEnoughAway(spawnPos);
        }

        CreateSun(spawnPos);
    }



    bool IsFarEnoughAway(Vector3 position)
    {
        var dist = 0f;
        var count = 0;

        foreach (Planet existingPlanet in Planet.Planets)
        {
            count++;
            dist = (existingPlanet.transform.position - position).sqrMagnitude;
            if (dist < Planet.maxPlanetRadius * 2)
            {
                return false;
            }
        }

        return true;
    }



    void CreatePlanet(int faction, Vector3 position)
    {
        var newPlanet = Instantiate(planet, position, Quaternion.identity);

        var randomness = faction != 0 ? 0 : Random.Range(0, 3);
        var p = newPlanet.GetComponent<Planet>();
        p.Generate(randomness, faction);
    }



    void CreateSun(Vector3 position)
    {
        Instantiate(sun, position, Quaternion.identity);
    }
    #endregion
}
