using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateBotColonize : State
{





    void Start()
    {
        blackBoard["ColonizeState"] = this;
    }



    public override void RunState()
    {

    }



    public override void StartState()
    {
        var faction = ((BotController)blackBoard["Controller"]).faction;

        var myPlanets = Planet.GetMyPlanets(faction);

		if (myPlanets.Count < 1)
		{
			return;
		}

        var attackPlanet = myPlanets[Random.Range(0, myPlanets.Count)];

        var closetPlanet = Planet.GetClosetPlanetToPoint(attackPlanet.transform.position, faction, true);

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
        return (State)blackBoard["BuildState"];
    }
}
