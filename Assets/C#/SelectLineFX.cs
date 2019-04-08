using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLineFX : MonoBehaviour
{
    #region Fields
    LineRenderer lineRend;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        // init
        lineRend = GetComponent<LineRenderer>();
    }



    public void DrawLine(Planet attackPlanet, Planet targetPlanet)
    {
        lineRend.positionCount = 2;
        Vector3[] points = new Vector3[2];

        // calc start and end
        points[0] = attackPlanet.transform.position;
        points[1] = targetPlanet.transform.position;

        // set points to line renderer
        lineRend.SetPositions(points);
    }



    public void ClearLine()
    {
        lineRend.positionCount = 0;
    }
    #endregion
}
