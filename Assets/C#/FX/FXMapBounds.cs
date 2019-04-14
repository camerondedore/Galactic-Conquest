using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXMapBounds : MonoBehaviour
{
    #region Fields
    [SerializeField] int resolution = 60;

    LineRenderer lineRend;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
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
            var point = transform.position + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 
                0, 
                Mathf.Sin(angle * Mathf.Deg2Rad)) * (MapGenerator.MapRadius + Planet.maxPlanetRadius);
            points.Add(point);

            angle += angleDelta;
        }

        lineRend.SetPositions(points.ToArray());
    }
    #endregion
}