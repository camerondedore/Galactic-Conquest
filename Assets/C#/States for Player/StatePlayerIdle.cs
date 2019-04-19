using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerIdle : State
{

    [SerializeField] Texture2D unkownCursor = null,
        neutralCursor = null,
        friendCursor = null,
        hostileCursor = null;



    void Start()
    {
        blackBoard["IdleState"] = this;
    }



    public override void RunState()
    {
        var hitPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

        if (hitPlanet == null)
        {
            Cursor.SetCursor(unkownCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == 0)
        {
            Cursor.SetCursor(neutralCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else if (hitPlanet.Faction == PlayerController.faction)
        {
            Cursor.SetCursor(friendCursor, Vector2.one * PlayerController.cursorHotspotSize, CursorMode.ForceSoftware);
        }
        else
        {
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
        if (((PlayerController)blackBoard["Controller"]).hitPlanet == null)
        {
            return this;
        }

        if (Input.GetAxisRaw("Burst") > 0 && ((PlayerController) blackBoard["Controller"]).hitPlanet.Faction == PlayerController.faction)
        {
            ((PlayerController)blackBoard["Controller"]).AttackPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

            return (State)blackBoard["BurstState"];
        }

		if (Input.GetAxisRaw("Feed") > 0 && ((PlayerController)blackBoard["Controller"]).hitPlanet.Faction == PlayerController.faction)
		{
			((PlayerController)blackBoard["Controller"]).AttackPlanet = ((PlayerController)blackBoard["Controller"]).hitPlanet;

			return (State)blackBoard["FeedState"];
		}

		return this;
    }
}
