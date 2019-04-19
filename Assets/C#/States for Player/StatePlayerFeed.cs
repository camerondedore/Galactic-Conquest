using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerFeed : State
{

    [SerializeField] Texture2D unkownSupplyCursor = null,
        neutralMissileSupplyCursor = null,
        friendParatrooperSupplyCursor = null,
        hostileSupplyCursor = null;



    void Start()
    {
        blackBoard["FeedState"] = this;
    }



    public override void RunState()
    {
        var hitPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

        if (hitPlanet == null || hitPlanet == ((PlayerController)blackBoard["Controller"]).AttackPlanet)
        {
            Cursor.SetCursor(unkownSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == 0)
        {
            Cursor.SetCursor(neutralMissileSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == PlayerController.faction)
        {
            Cursor.SetCursor(friendParatrooperSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(hostileSupplyCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
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

        if (Input.GetAxisRaw("Feed") == 0)
        {
            if (((PlayerController)blackBoard["Controller"]).TargetPlanet != null)
            {
                ((PlayerController)blackBoard["Controller"]).AttackSelectedPlanet(false);
            }
            else
            {
                ((PlayerController)blackBoard["Controller"]).AttackPlanet.SetFeed(null);
            }

            ((PlayerController)blackBoard["Controller"]).ClearSelectedPlanets();
            return (State)blackBoard["IdleState"];
        }

        return this;
    }
}
