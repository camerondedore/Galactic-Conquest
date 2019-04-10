using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject[] payloads = null;

    float timeBetweenLaunches = 0.2f;
    Planet myPlanet;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        myPlanet = transform.root.GetComponent<Planet>();
    }



    public void Fire(int amt, Planet targetPlanet)
    {
        // add to launch deck
        StartCoroutine(Launch(amt, targetPlanet, myPlanet.Faction));
    }



    IEnumerator Launch(int amt, Planet targetPlanet, int faction)
    {
        var indexToLaunch = 0;

        // calc scale
        var scale = Mathf.Clamp(Mathf.FloorToInt(amt / 50), 1, 10);
        var myScale = 1;

        while (amt > 0 && faction == myPlanet.Faction)
        {
            myScale = Mathf.Clamp(scale, 1, amt);

            // detect type to launch
            if (targetPlanet.Population > 0 && targetPlanet.Faction != myPlanet.Faction)
            {
                indexToLaunch = 0;
            }
            else
            {
                indexToLaunch = 1;
            }

            // launch
            Vector3 launchPad = Random.onUnitSphere * myPlanet.Radius;

            GameObject payload = Instantiate(payloads[indexToLaunch], transform.position + launchPad, Quaternion.LookRotation(launchPad)) as GameObject;
            ILaunch m = payload.GetComponent<ILaunch>();
            m.Launch(targetPlanet, myPlanet.Faction, myScale);

            amt -= myScale;

            yield return new WaitForSeconds(timeBetweenLaunches / (Mathf.Clamp(amt, 1, 200)));
        }
    }
    #endregion
}
