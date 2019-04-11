using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour, IFaction
{

    #region Fields
    public static List<Planet> Planets = new List<Planet>();
    public static int maxPlanetRadius = 5;

    public int growthRate = 1;

    [SerializeField] int population = 0,
        faction = 0;
    [Space]
    [SerializeField] TextMeshPro popText = null;
    [SerializeField] LineRenderer haloFX = null;
    [SerializeField] Launcher launchPad = null;
    [SerializeField] Material[] planetMaterials = null;

    [SerializeField] Planet feedTargetPlanet = null;
    float growthTimer = 0,
        feedTimer = 0,
        radius = .5f,
        timeBetweenFeed = 1;
    int populationCap = 100;
    #endregion

    #region Properties
    public int Population
    {
        get
        {
            return population;
        }

        set
        {
            population = Mathf.Clamp(value, 0, populationCap);
            popText.text = population.ToString();
        }
    }

    public int Faction
    {
        get
        {
            return faction;
        }

        set
        {
            faction = value;
            popText.color = FXFactionColor.factionColors[faction];
            feedTargetPlanet = null;
        }
    }

    public float Radius
    {
        get
        {
            return radius;
        }

        set
        {
            radius = value;
            transform.localScale = Vector3.one * 2 * value;
        }
    }
    #endregion



    #region Methods
    void Awake()
    {
        Population = population;
        Faction = faction;
        Deselect();
        Planets.Add(this);
    }



    void OnDisable()
    {
        Planets.Remove(this);
    }



    void Update()
    {
        Grow();
        Feed();
    }



    void Grow()
    {
        growthTimer += Time.deltaTime;

        if (growthTimer > 1 && faction != 0)
        {
            growthTimer = 0;
            Population += growthRate;
        }
    }



    void Feed()
    {
        if (feedTargetPlanet == null || Population == 0)
        {
            return;
        }

        feedTimer += Time.deltaTime;

        if (feedTimer > timeBetweenFeed && faction != 0)
        {
            feedTimer = 0;
            Population--;
            launchPad.Fire(1, feedTargetPlanet);
        }
    }



    public void Damage(int attackerFaction, int damage)
    {
        if (attackerFaction != Faction)
        {
            Population -= damage;

            if (Population == 0)
            {
                Faction = attackerFaction;
                launchPad.StopAllCoroutines();
            }
        }
        else
        {
            Population += damage;
        }
    }



    public void Select()
    {
        haloFX.enabled = true;
    }



    public void Deselect()
    {
        haloFX.enabled = false;
    }



    public void Attack(Planet targetPlanet)
    {
        var amt = Mathf.FloorToInt((Population * 0.5f));

        if (Population - amt < 1)
        {
            return;
        }

        Population -= amt;

        launchPad.Fire(amt, targetPlanet);
    }



    public void SetFeed(Planet targetPlanet)
    {
        feedTargetPlanet = targetPlanet;
    }



    public void Generate(int random, int newFaction)
    {
        random = Mathf.Clamp(random, 0, 2);

        var value = Random.Range(3 - random, 3 + random + 1);
        value = Mathf.Clamp(value, 1, maxPlanetRadius);

        Faction = newFaction;
        Radius = value * 0.5f;
        growthRate = value;
        populationCap = value * 100;

        if (faction != 0)
        {
            Population = value * 5;
        }

        timeBetweenFeed = (2f / growthRate);

        var direction = Random.onUnitSphere;
        direction.y *= .1f;
        transform.rotation = Quaternion.LookRotation(direction);

        GetComponent<Renderer>().material = planetMaterials[Random.Range(0, planetMaterials.Length)];
    }



    public int GetFaction()
    {
        return Faction;
    }



    public static Planet GetClosetPlanetToPoint(Vector3 point, int myFaction, bool isNeutral)
    {
        Planet closestPlanet = null;
        var smallestDist = Mathf.Infinity;

        foreach (Planet p in Planets)
        {
            if (p.faction == myFaction)
            {
                continue;
            }

            if ((p.faction == 0) == !isNeutral)
            {
                continue;
            }

            var distanceSquared = (point - p.transform.position).sqrMagnitude;

            if (distanceSquared < smallestDist)
            {
                smallestDist = distanceSquared;
                closestPlanet = p;
            }
        }

        return closestPlanet;
    }



    public static List<Planet> GetMyPlanets(int myFaction)
    {
        return Planets.Where(p => p.Faction == myFaction).ToList();
    }



    public static int GetCountOfMyPlanets(int myFaction)
    {
        return Planets.Where(p => p.Faction == myFaction).ToList().Count;
    }
    #endregion
}
