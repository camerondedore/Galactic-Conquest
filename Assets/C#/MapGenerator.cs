using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    #region Fields
    [SerializeField] int factionCount = 4;
    [SerializeField] float mapRadius = 10;
    [SerializeField] float mapHeight = 1;
    [SerializeField] int minPlanets = 10;
    [SerializeField] int maxPlanets = 15;
    [SerializeField] GameObject planet = null;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Awake()
    {
        // spawn player planets;
        int playerNumber = 1;

        while (playerNumber <= factionCount)
        {
            var angle = Random.Range(0f, 6.28f);
            var spawnPos = new Vector3(Mathf.Cos(angle), Random.Range(-mapHeight, mapHeight), Mathf.Sin(angle)) * mapRadius;

            CreatePlanet(playerNumber, spawnPos);

            playerNumber++;
        }

        // spawn gaia planets
        int count = Random.Range(minPlanets, maxPlanets + 1);

        while (count > 0)
        {
            var spawnPos =  new Vector3(Mathf.Round(Random.Range(-mapRadius, mapRadius)), 
                Random.Range(-mapHeight, mapHeight),
                Mathf.Round(Random.Range(-mapRadius, mapRadius)));

            CreatePlanet(0, spawnPos);

            count--;
        }
    }



    void CreatePlanet(int faction, Vector3 position)
    {
        // spawn planet
        position.y = Mathf.Clamp(position.y, -mapHeight, mapHeight);
        GameObject newPlanet = Instantiate(planet, position, Quaternion.identity) as GameObject;

        // generate planet
        var randomness = faction != 0 ? 0 : Random.Range(0, 3);
        var p = newPlanet.GetComponent<Planet>();
        p.Generate(randomness, faction);
    }
    #endregion
}
