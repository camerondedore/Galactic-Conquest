using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    #region Fields
    Transform mainCam;
    Vector3 cameraDir;
    int moveAreaWidth = 50;
    float moveSpeed = 20,
        zoomSpeed = 100,
        maxZoom = 30,
        minZoom = 4,
        zoom = 10,
        multiplier = 3;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        mainCam = GetComponentInChildren<Camera>().transform;
        cameraDir = (mainCam.transform.position - transform.position).normalized;

        mainCam.localPosition = cameraDir * zoom;
        var startingPos = Planet.Planets.Where(p => p.Faction == 1).First().transform.position;
        startingPos.y = 0;
        transform.position =  startingPos;
    }



    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }



    void ZoomCamera()
    {
        if (Input.GetAxisRaw("Zoom") != 0)
        {
            zoom -= Input.GetAxisRaw("Zoom") * zoomSpeed * Time.deltaTime;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        }

        mainCam.localPosition = Vector3.Lerp(mainCam.localPosition, cameraDir * zoom, 3 * Time.deltaTime);
    }



    void MoveCamera()
    {
        // mouse
        var mouse2dCoord = Input.mousePosition;
        var x = mouse2dCoord.x < moveAreaWidth ? -1 : (mouse2dCoord.x > (Screen.width - moveAreaWidth) ? 1 : 0);
        var z = mouse2dCoord.y < moveAreaWidth ? -1 : (mouse2dCoord.y > (Screen.height - moveAreaWidth) ? 1 : 0);

        // keyboard
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            x = Mathf.CeilToInt(Input.GetAxisRaw("Horizontal"));
            z = Mathf.CeilToInt(Input.GetAxisRaw("Vertical"));
        }

        var mult = Mathf.Clamp(Input.GetAxisRaw("Fast Move") * multiplier, 1, multiplier);

        transform.position += new Vector3(x, 0, z).normalized * Time.deltaTime * moveSpeed * mult;
    }
    #endregion
}
