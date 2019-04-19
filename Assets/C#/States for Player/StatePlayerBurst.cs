using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerBurst : State
{

    [SerializeField] Texture2D unkownCursor = null,
        neutralMissileCursor = null,
        friendParatrooperCursor = null,
        hostileMissileCursor = null;



    void Start()
    {
        blackBoard["BurstState"] = this;
    }



    public override void RunState()
    {
        var hitPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

        if (hitPlanet == null || hitPlanet == ((PlayerController)blackBoard["Controller"]).AttackPlanet)
        {
            Cursor.SetCursor(unkownCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == 0)
        {
            Cursor.SetCursor(neutralMissileCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == PlayerController.faction)
        {
            Cursor.SetCursor(friendParatrooperCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(hostileMissileCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }


        if (hitPlanet != ((PlayerController)blackBoard["Controller"]).AttackPlanet)
        {
            ((PlayerController)blackBoard["Controller"]).TargetPlanet = hitPlanet;
        }
        else
        {
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
        if (((PlayerController)blackBoard["Controller"]).AttackPlanet.Faction != PlayerController.faction)
        {
            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        if (Input.GetAxisRaw("Burst") == 0)
        {
            if (((PlayerController)blackBoard["Controller"]).TargetPlanet != null)
            {
                ((PlayerController)blackBoard["Controller"]).AttackSelectedPlanet(true);
            }

            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        return this;
    }
}
