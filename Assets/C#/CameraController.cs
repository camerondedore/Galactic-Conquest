using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    #region Fields
    Transform mainCam;
    float moveSpeed = 20;
    float zoomSpeed = 100;
    int moveAreaWidth = 50;
    float maxZoom = 30;
    float minZoom = 4;
    Vector3 cameraDir;
    float zoom = 10;
    float multiplier = 3;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        mainCam = GetComponentInChildren<Camera>().transform;
        cameraDir = (mainCam.transform.position - transform.position).normalized;

        // init
        mainCam.localPosition = cameraDir * zoom;
        transform.position = Planet.Planets.Where(p => p.Faction == 1).First().transform.position;
    }



    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }



    void ZoomCamera()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            zoom -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
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
