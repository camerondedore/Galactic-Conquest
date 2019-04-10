using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Fields
    public static int planetCount = 0;

    [SerializeField] int factionCount = 4;
    [SerializeField] float mapRadius = 10;
    [SerializeField] float mapHeight = 1;
    [SerializeField] int minPlanets = 10;
    [SerializeField] int maxPlanets = 15;
    [SerializeField] GameObject planet = null;
    [SerializeField] GameObject sun = null;
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
        // spawn player planets;
        int playerNumber = 1;
        planetCount += factionCount;
        Vector3 spawnPos = Vector3.zero;
        float playerAngle = 6.28f / factionCount;
        float phaseShift = Random.Range(0f, 6.28f);

        while (playerNumber <= factionCount)
        {
            var angle = playerAngle * playerNumber + phaseShift;
            spawnPos = Vector3.zero;

            // calc spawn pos
            spawnPos = new Vector3(Mathf.Cos(angle) * mapRadius, Random.Range(-mapHeight, mapHeight), Mathf.Sin(angle) * mapRadius);

            // create planet
            CreatePlanet(playerNumber, spawnPos);

            playerNumber++;
        }
    }



    void SpawnGaiaPlanets()
    {
        // spawn gaia planets
        int count = Random.Range(minPlanets, maxPlanets + 1);
        planetCount += count;

        while (count > 0)
        {
            Vector3 spawnPos = Vector3.zero;

            // check planet distances
            var validPos = false;

            while (!validPos)
            {
                // calc spawn pos
                spawnPos = new Vector3(Mathf.Round(Random.Range(-mapRadius, mapRadius)),
                Random.Range(-mapHeight, mapHeight),
                Mathf.Round(Random.Range(-mapRadius, mapRadius)));

                // check spawn pos
                validPos = IsFarEnoughAway(spawnPos);
            }

            // create planet
            CreatePlanet(0, spawnPos);

            count--;
        }
    }



    void SpawnSun()
    {
        Vector3 spawnPos = Vector3.zero;
        bool validPos = false;

        while (!validPos)
        {
            // calc spawn pos
            spawnPos = new Vector3(Mathf.Round(Random.Range(-mapRadius, mapRadius)),
            Random.Range(-mapHeight, mapHeight),
            Mathf.Round(Random.Range(-mapRadius, mapRadius)));

            // check spawn pos
            validPos = IsFarEnoughAway(spawnPos);
        }

        // create sun
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
        // spawn planet
        GameObject newPlanet = Instantiate(planet, position, Quaternion.identity) as GameObject;

        // generate planet
        var randomness = faction != 0 ? 0 : Random.Range(0, 3);
        var p = newPlanet.GetComponent<Planet>();
        p.Generate(randomness, faction);
    }



    void CreateSun(Vector3 position)
    {
        // spawn sun
        Instantiate(sun, position, Quaternion.identity);
    }
    #endregion
}
