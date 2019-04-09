using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BotController : MonoBehaviour
{
    #region Fields
    [SerializeField] int faction = 2;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Update()
    {
        // get my planet
        var me = Planet.Planets.Where(p => p.Faction == faction && Random.Range(0, 10) < 3).FirstOrDefault();


        // get target
        var them = Planet.Planets.Where(p => p.Faction != faction && Random.Range(0, 10) < 3).FirstOrDefault();

        // attack
        if (me != null && them != null && Mathf.Floor(Time.time) % 10 == 0)
        {
            me.Attack(them);
        }
    }
    #endregion
}
