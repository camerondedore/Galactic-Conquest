using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour, IFaction
{

    #region Fields
    public static List<Planet> Planets = new List<Planet>();

    public int growthRate = 1;

    [SerializeField] float growthTimer = 0;
    [SerializeField] int population = 0;
    [SerializeField] int faction = 0;
    [Space]
    [SerializeField] TextMeshPro popText = null;
    [SerializeField] LineRenderer haloFX = null;
    [SerializeField] Launcher launchPad = null;

    float radius = .5f;
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
            population = Mathf.Clamp(value, 0, int.MaxValue);
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
            //var newLocalPos = popText.transform.localPosition;
            //newLocalPos.y = value + 1;
            //popText.transform.localPosition = newLocalPos;
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
        Population -= amt;

        // fire
        launchPad.Fire(amt, targetPlanet);
    }



    public void Generate(int random, int newFaction)
    {
        random = Mathf.Clamp(random, 0, 2);

        var value = Random.Range(3 - random, 3 + random);

        // set planet properties
        Faction = newFaction;
        Radius = value * 0.5f;
        growthRate = value;
        if (faction != 0)
        {
            Population = value * 5;
        }
    }



    public int GetFaction()
    {
        return Faction;
    }
    #endregion
}
