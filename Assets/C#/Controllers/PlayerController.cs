using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IInitializeIntoBlackBoard
{
    #region Fields
    public static int cursorHotspotSize = 64,
		faction = 1;
	public static float actionTime = 3;

    public Planet attackPlanet,
        targetPlanet,
        hitPlanet = null;

    [Space]
    [SerializeField] LayerMask planetMask = 0;
    [SerializeField] FXSelectLine lineFX = null;

    Camera mainCam = null;
    RaycastHit mouseHit;
	float lastActionTime = 0,
		actionAdaptSpeed = .1f,
		minActionTime = 1f,
		maxActionTime = 6f;
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
                    lineFX.DrawLine(attackPlanet, targetPlanet);
                }
            }
        }
    }
	#endregion



	#region Methods
	void Start()
	{
		mainCam = Camera.main;
	}



    public void AttackSelectedPlanet(bool burst)
    {
		actionTime = Mathf.Lerp(actionTime, Time.time - lastActionTime, actionAdaptSpeed);
		actionTime = Mathf.Clamp(actionTime, minActionTime, maxActionTime);
		lastActionTime = Time.time;

        if (burst)
        {
            AttackPlanet.Attack(TargetPlanet);
        }
        else
        {
            AttackPlanet.SetFeed(TargetPlanet);
        }

        ClearSelectedPlanets();
    }



    public void ClearSelectedPlanets()
    {
        AttackPlanet = null;
        TargetPlanet = null;
    }



    void Update()
    {
		if (Time.timeScale > 0)
		{
			hitPlanet = GetPlanetUnderMouse();
		}
		else
		{
			hitPlanet = null;
		}
    }



    public Planet GetPlanetUnderMouse()
    {
        var mouse3dCoord = GetMouseWorldCoord(1000);

        var rayDir = mouse3dCoord - mainCam.transform.position;
        Physics.Raycast(mainCam.transform.position, rayDir, out mouseHit, 1000, planetMask);

        var hitCollider = mouseHit.collider;
        if (hitCollider != null)
        {
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
