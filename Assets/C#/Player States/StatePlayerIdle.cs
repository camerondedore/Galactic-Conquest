using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerIdle : State
{

    [SerializeField] Texture2D unkownCursor = null;
    [SerializeField] Texture2D neutralCursor = null;
    [SerializeField] Texture2D friendCursor = null;
    [SerializeField] Texture2D hostileCursor = null;



    void Start()
    {
        blackBoard["IdleState"] = this;
    }



    public override void RunState()
    {
        // set cursor
        var hitPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

        if (hitPlanet == null)
        {
            // nothing
            Cursor.SetCursor(unkownCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == 0)
        {
            // neutral
            Cursor.SetCursor(neutralCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == PlayerController.faction)
        {
            // friend
            Cursor.SetCursor(friendCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else
        {
            // hostile
            Cursor.SetCursor(hostileCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
    }



    public override void StartState()
    {

    }



    public override void EndState()
    {

    }



    public override State Transition()
    {
        // no planet
        if (((PlayerController)blackBoard["Controller"]).hitPlanet == null)
        {
            return this;
        }

        // if clicking on player planet
        if (Input.GetAxisRaw("Select") > 0 && ((PlayerController) blackBoard["Controller"]).hitPlanet.Faction == PlayerController.faction)
        {
            // set attack planet
            ((PlayerController)blackBoard["Controller"]).AttackPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;
            // go to select
            return (State)blackBoard["SelectState"];
        }

        return this;
    }
}
