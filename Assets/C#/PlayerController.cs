using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Fields
    public Planet attackPlanet;
    public Planet targetPlanet;
    public static int faction = 1;
    [Space]
    [SerializeField] Camera mainCam = null;
    [SerializeField] LayerMask planetMask = 0;
    [SerializeField] SelectLineFX lineFX = null;
    [SerializeField] Texture2D selectCursor;
    [SerializeField] Texture2D AttackCursor;
    [SerializeField] Texture2D ReinforceCursor;

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
    void Update()
    {
        SelectAttackPlanet();
        SelectTargetPlanet();
        AttackSelectedPlanet();
        CursorUpdate();
    }



    void CursorUpdate()
    {
        var hitPlanet = GetPlanetUnderMouse();

        // no planet found
        if (hitPlanet == null)
        {
            Cursor.SetCursor(selectCursor, Vector2.one * 32, CursorMode.ForceSoftware);
        }
        else if (attackPlanet != null && attackPlanet != hitPlanet)
        {   if (hitPlanet.Faction != faction)
            {
                Cursor.SetCursor(AttackCursor, Vector2.one * 32, CursorMode.ForceSoftware);
            }
            if (hitPlanet.Faction == faction)
            {
                Cursor.SetCursor(ReinforceCursor, Vector2.one * 32, CursorMode.ForceSoftware);
            }
        }
    }



    void SelectAttackPlanet()
    {
        // get attacking planet
        if (Input.GetButtonDown("Fire1"))
        {
            var hitPlanet = GetPlanetUnderMouse();

            // no planet found
            if (hitPlanet == null)
            {
                return;
            }

            if (hitPlanet.Faction == faction)
            {
                AttackPlanet = hitPlanet;
            }
        }
    }



    void SelectTargetPlanet()
    {
        // can't select target without attack planet
        if (AttackPlanet == null)
        {
            return;
        }

        // get target planet
        if (Input.GetButton("Fire1"))
        {
            var hitPlanet = GetPlanetUnderMouse();

            // no planet found
            if (hitPlanet == null)
            {
                TargetPlanet = null;
                return;
            }

            if (hitPlanet != attackPlanet)
            {
                TargetPlanet = hitPlanet;
            }
        }
    }



    void AttackSelectedPlanet()
    {
        // attack planet
        if (Input.GetButtonUp("Fire1"))
        {
            // no planets selected
            if (attackPlanet == null || targetPlanet == null)
            {
                // clear selected planets
                AttackPlanet = null;
                TargetPlanet = null;
                return;
            }

            // attack code here
            attackPlanet.Attack(TargetPlanet);

            // clear selected planets
            AttackPlanet = null;
            TargetPlanet = null;
        }
    }



    Planet GetPlanetUnderMouse()
    {
        // get clicked on point
        var mouse3dCoord = GetMouseWorldCoord(1000);

        // cast ray to find planet
        var rayDir = mouse3dCoord - mainCam.transform.position;
        Physics.Raycast(mainCam.transform.position, rayDir, out mouseHit, 1000, planetMask);

        Debug.DrawLine(mainCam.transform.position, mainCam.transform.position + rayDir);

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
    #endregion
}
