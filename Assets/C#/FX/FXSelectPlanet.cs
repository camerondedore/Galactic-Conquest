using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXSelectPlanet : MonoBehaviour
{
    #region Fields
    [SerializeField] int resolution = 20;

    LineRenderer lineRend;
    Planet myPlanet;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        // init
        lineRend = GetComponent<LineRenderer>();
        myPlanet = transform.root.GetComponent<Planet>();
        DrawCircle();
    }



    void DrawCircle()
    {
        lineRend.positionCount = resolution;
        List<Vector3> points = new List<Vector3>();
        float angle = 0;
        float angleDelta = 360 / resolution;

        while (angle < 360)
        {
            // calc new point
            var point = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * myPlanet.Radius * 1.1f;
            points.Add(point);

            angle += angleDelta;
        }

        // set points to line renderer
        lineRend.SetPositions(points.ToArray());
    }
    #endregion
}
