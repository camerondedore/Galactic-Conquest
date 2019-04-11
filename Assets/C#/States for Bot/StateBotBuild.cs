using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StateBotBuild : State
{

    Vector2 timeRange = new Vector2(1f, 6f);
    float endTime;



    void Start()
    {
        blackBoard["BuildState"] = this;
    }



    public override void RunState()
    {
        var faction = ((BotController)blackBoard["Controller"]).faction;

        if (Planet.GetCountOfMyPlanets(faction) < 1)
        {
            gameObject.SetActive(false);
        }
    }



    public override void StartState()
    {
        endTime = Time.time + Random.Range(timeRange.x, timeRange.y);
    }



    public override void EndState()
    {

    }



    public override State Transition()
    {
        var roll = Random.Range(0f, 1f);

        var faction = ((BotController)blackBoard["Controller"]).faction;

        var raidChance = Mathf.Clamp(Planet.GetCountOfMyPlanets(faction) / ((float) Planet.Planets.Count), 0.15f, 1);

        if (Time.time > endTime)
        {
            return roll > raidChance ? (State)blackBoard["ColonizeState"] : (State)blackBoard["RaidState"];
        }

        return this;
    }
}
