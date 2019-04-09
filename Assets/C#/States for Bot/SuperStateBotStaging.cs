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
        // run sub state
        subState.RunState();

        // check sub state for transitions
        State nextState = subState.Transition();

        // transition substate
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
        // has half of their proportion of planets
        if (((BotController)blackBoard["Controller"]).GetMapOwnership() > .5f / MapGenerator.planetCount)
        {
            // skirmish mode
            //return (State)blackBoard["SkirmishState"];
        }

        return this;
    }
}
