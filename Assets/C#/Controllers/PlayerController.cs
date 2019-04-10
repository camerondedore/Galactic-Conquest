using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInitializeIntoBlackBoard
{
    #region Fields
    public Planet attackPlanet;
    public Planet targetPlanet;

    [Space]
    [SerializeField] Camera mainCam = null;
    [SerializeField] LayerMask planetMask = 0;
    [SerializeField] FXSelectLine lineFX = null;


    // new
    public static int cursorHotspotSize = 64;
    public static int faction = 1;

    public Planet hitPlanet = null;

    RaycastHit mouseHit;
    #endregion

    #region Properties
    public Planet AttackPlanet
    {
        get
        {
            return attackPlanet;
        }

        set
        {
            if (value == null)
            {
                if (attackPlanet != null)
                {
                    attackPlanet.Deselect();
                }
                attackPlanet = value;
            }
            else
            {
                if (attackPlanet != value)
                {
                    if (attackPlanet != null)
                    {
                        attackPlanet.Deselect();
                    }

                    attackPlanet = value;
                    attackPlanet.Select();
                }
            }
        }
    }

    public Planet TargetPlanet
    {
        get
        {
            return targetPlanet;
        }

        set
        {
            if (value == null)
            {
                if (targetPlanet != null)
                {
                    targetPlanet.Deselect();
                    lineFX.ClearLine();
                }
                targetPlanet = value;
            }
            else
            {
                if (targetPlanet != value)
                {
                    if (targetPlanet != null)
                    {
                        targetPlanet.Deselect();
                    }

                    targetPlanet = value;
                    targetPlanet.Select();
                    // draw selector line
                    lineFX.DrawLine(attackPlanet, targetPlanet);
                }
            }
        }
    }
    #endregion



    #region Methods
    public void AttackSelectedPlanet(bool burst)
    {
        if (burst)
        {
            // burst
            AttackPlanet.Attack(TargetPlanet);
        }
        else
        {
            // feed
            AttackPlanet.SetFeed(TargetPlanet);
        }

        // clear selected planets
        ClearSelectedPlanets();
    }



    public void ClearSelectedPlanets()
    {
        AttackPlanet = null;
        TargetPlanet = null;
    }



    void Update()
    {
        hitPlanet = GetPlanetUnderMouse();
    }



    public Planet GetPlanetUnderMouse()
    {
        // get clicked on point
        var mouse3dCoord = GetMouseWorldCoord(1000);

        // cast ray to find planet
        var rayDir = mouse3dCoord - mainCam.transform.position;
        Physics.Raycast(mainCam.transform.position, rayDir, out mouseHit, 1000, planetMask);

        //Debug.DrawLine(mainCam.transform.position, mainCam.transform.position + rayDir);

        // something was hit
        var hitCollider = mouseHit.collider;
        if (hitCollider != null)
        {
            // planet was hit
            var hitPlanet = hitCollider.GetComponent<Planet>();
            if (hitPlanet != null)
            {
                return hitPlanet;
            }
        }

        return null;
    }



    Vector3 GetMouseWorldCoord(float y)
    {
        // get clicked on point
        var mouse2dCoord = Input.mousePosition;
        var mouse2dCoordDepth = new Vector3(mouse2dCoord.x, mouse2dCoord.y, y);
        var mouse3dCoord = mainCam.ScreenToWorldPoint(mouse2dCoordDepth);

        return mouse3dCoord;
    }



    public string GetBlackBoardKey()
    {
        return "Controller";
    }
    #endregion
}
