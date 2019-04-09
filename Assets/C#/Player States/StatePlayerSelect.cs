using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerSelect : State
{

    [SerializeField] Texture2D unkownCursor = null;
    [SerializeField] Texture2D neutralMissileCursor = null;
    [SerializeField] Texture2D friendParatrooperCursor = null;
    [SerializeField] Texture2D hostileMissileCursor = null;



    void Start()
    {
        blackBoard["SelectState"] = this;
    }



    public override void RunState()
    {
        // set cursor
        var hitPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

        if (hitPlanet == null || hitPlanet == ((PlayerController)blackBoard["Controller"]).AttackPlanet)
        {
            // nothing
            Cursor.SetCursor(unkownCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == 0)
        {
            // neutral
            Cursor.SetCursor(neutralMissileCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == PlayerController.faction)
        {
            // friend
            Cursor.SetCursor(friendParatrooperCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else
        {
            // hostile
            Cursor.SetCursor(hostileMissileCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }


        // hit planet is not attack planet
        if (hitPlanet != ((PlayerController)blackBoard["Controller"]).AttackPlanet)
        {
            // set target planet
            ((PlayerController)blackBoard["Controller"]).TargetPlanet = hitPlanet;
        }
        else
        {
            // set target planet to null
            ((PlayerController)blackBoard["Controller"]).TargetPlanet = null;
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
        // lost attack planet
        if (((PlayerController)blackBoard["Controller"]).AttackPlanet != null &&
            ((PlayerController)blackBoard["Controller"]).AttackPlanet.Faction != PlayerController.faction)
        {
            // go to idle
            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        // supply mode
        if (Input.GetAxisRaw("Select") > 0 && Input.GetAxisRaw("Feed") > 0)
        {
            return (State)blackBoard["SupplyState"];
        }

        // stop clicking
        if (Input.GetAxisRaw("Select") == 0)
        {
            if (((PlayerController)blackBoard["Controller"]).TargetPlanet != null)
            {
                // attack
                ((PlayerController)blackBoard["Controller"]).AttackSelectedPlanets(true);
            }

            // go to idle
            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        // still clicking
        return this;
    }
}
