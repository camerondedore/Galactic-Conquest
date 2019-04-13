using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Fields
    public static int planetCount = 0;

    public float mapRadius = 10;

    [SerializeField] GameObject planet = null,
        sun = null;
    [SerializeField] float mapHeight = 1;
    [SerializeField] int factionCount = 4,
        minPlanets = 10,
        maxPlanets = 15;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Awake()
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
