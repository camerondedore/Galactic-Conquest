using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperStateBotStaging : SuperState
{





    void Start()
    {
        blackBoard["StagingState"] = this;
    }



    public override void RunState()
    {
        subState.RunState();

        State nextState = subState.Transition();

        SetState(nextState);
    }



    public override void StartState()
    {
        base.StartState();
    }



    public override void EndState()
    {
        base.EndState();
    }



    public override State Transition()
    {
        /*if (((BotController)blackBoard["Controller"]).GetMapOwnership() > .5f / MapGenerator.planetCount)
        {
            return (State)blackBoard["SkirmishState"];
        }*/

        return this;
    }
}
