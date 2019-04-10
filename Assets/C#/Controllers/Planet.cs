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

    [SerializeField] int population = 0;
    [SerializeField] int faction = 0;
    [Space]
    [SerializeField] TextMeshPro popText = null;
    [SerializeField] LineRenderer haloFX = null;
    [SerializeField] Launcher launchPad = null;
    [SerializeField] Material[] planetMaterials = null;

    float growthTimer = 0;
    float feedTimer = 0;
    float radius = .5f;
    int populationCap = 100;
    Planet feedTargetPlanet = null;
    float timeBetweenFeed = 1;
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
            // update display
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
            // update display
            popText.color = FXFactionColor.factionColors[faction];
            // clear planets
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
        // init
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
        // grow population
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

        // feed target
        feedTimer += Time.deltaTime;

        if (feedTimer > timeBetweenFeed && faction != 0)
        {
            feedTimer = 0;
            Population--;
            launchPad.Fire(1, feedTargetPlanet);
        }
    }



    public void Damage(int damage)
    {
        // damage population
        Population -= damage;

        // remove faction
        if (population == 0)
        {
            Faction = 0;
            launchPad.StopAllCoroutines();
        }
    }



    public void Invade(int invadingFaction, int forceSize)
    {
        // add population
        Population += forceSize;

        // stop feed
        if (invadingFaction != faction)
        {
            feedTargetPlanet = null;
        }

        // change faction
        Faction = invadingFaction;
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
        // reduce popultaion
        var amt = Mathf.FloorToInt((Population * 0.5f));

        if (Population - amt < 1)
        {
            return;
        }

        Population -= amt;

        // fire
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

        // set planet properties
        Faction = newFaction;
        Radius = value * 0.5f;
        growthRate = value;
        populationCap = value * 100;
        if (faction != 0)
        {
            Population = value * 5;
        }
        timeBetweenFeed = (2f / growthRate);

        // rotate
        var direction = Random.onUnitSphere;
        direction.y *= .1f;
        transform.rotation = Quaternion.LookRotation(direction);

        // material
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
            // not my faction
            if (p.faction == myFaction)
            {
                continue;
            }

            // deal with neutrals
            if ((p.faction == 0) == !isNeutral)
            {
                continue;
            }

            // get closest
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
