using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BotController : MonoBehaviour, IInitializeIntoBlackBoard
{
    #region Fields
    public int faction = 2;
    #endregion

    #region Properties
    #endregion



    #region Methods
    public string GetBlackBoardKey()
    {
        return "Controller";
    }



    public float GetMapOwnership()
    {
        var countMine = 0;

        foreach (Planet p in Planet.Planets)
        {
            if (p.Faction == faction)
            {
                countMine++;
            }
        }

        return (float) countMine / Planet.Planets.Count;
    }
    #endregion
}
