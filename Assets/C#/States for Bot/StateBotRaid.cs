using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateBotRaid : State
{





    void Start()
    {
        blackBoard["RaidState"] = this;
    }



    public override void RunState()
    {

    }



    public override void StartState()
    {
        // my faction
        var faction = ((BotController)blackBoard["Controller"]).faction;

        // get my planets
        var myPlanets = Planet.GetMyPlanets(faction);
        // get attacker
        var attackPlanet = myPlanets[Random.Range(0, myPlanets.Count)];

        // get target
        var closetPlanet = Planet.GetClosetPlanetToPoint(attackPlanet.transform.position, faction, false);

        if (closetPlanet != null)
        {
            attackPlanet.Attack(closetPlanet);
        }
    }



    public override void EndState()
    {

    }



    public override State Transition()
    {
        // keep building
        return (State)blackBoard["BuildState"];
    }
}
