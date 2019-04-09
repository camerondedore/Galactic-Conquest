using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerSupply : State
{

    [SerializeField] Texture2D unkownSupplyCursor = null;
    [SerializeField] Texture2D neutralMissileSupplyCursor = null;
    [SerializeField] Texture2D friendParatrooperSupplyCursor = null;
    [SerializeField] Texture2D hostileSupplyCursor = null;



    void Start()
    {
        blackBoard["SupplyState"] = this;
    }



    public override void RunState()
    {
        // set cursor
        var hitPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

        if (hitPlanet == null || hitPlanet == ((PlayerController)blackBoard["Controller"]).AttackPlanet)
        {
            // nothing
            Cursor.SetCursor(unkownSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == 0)
        {
            // neutral
            Cursor.SetCursor(neutralMissileSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == PlayerController.faction)
        {
            // friend
            Cursor.SetCursor(friendParatrooperSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else
        {
            // hostile
            Cursor.SetCursor(hostileSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
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
        if (((PlayerController)blackBoard["Controller"]).AttackPlanet.Faction != PlayerController.faction)
        {
            // go to idle
            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        // select mode
        if (Input.GetAxisRaw("Select") > 0 && Input.GetAxisRaw("Feed") == 0)
        {
            return (State)blackBoard["SelectState"];
        }

        // stop clicking
        if (Input.GetAxisRaw("Select") == 0)
        {
            if (((PlayerController)blackBoard["Controller"]).TargetPlanet != null)
            {
                // attack
                ((PlayerController)blackBoard["Controller"]).AttackSelectedPlanets(false);
            }
            else
            {
                // clear attack planet's feed
                ((PlayerController)blackBoard["Controller"]).AttackPlanet.SetFeed(null);
            }

            // go to idle
            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        // still clicking
        return this;
    }
}
