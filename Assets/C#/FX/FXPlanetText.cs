using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXPlanetText : MonoBehaviour
{
    #region Fields
    public static Transform mainCam = null;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        // get main camera
        if (mainCam == null)
        {
            mainCam = Camera.main.transform;
        }

        // set rotation
        transform.forward = mainCam.forward;
    }
    #endregion
}
