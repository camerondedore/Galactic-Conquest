using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, ILaunch, IFaction
{
    #region Fields
    public Planet target;
    public int damage = 1;
    public float speed = 5;
    public float homeSpeed = 2;

    [SerializeField] GameObject explosionFX = null;

    Vector3 direction;
    Vector3 targetOffset;
    float startHomeDistance = 0.5f;
    float distanceTraveled = 0;
    int faction;
    #endregion

    #region Properties
    #endregion



    #region Methods
    public void Launch(Planet targetPlanet, int newFaction)
    {
        target = targetPlanet;
        faction = newFaction;

        // randomize
        speed *= Random.Range(.9f, 1.2f);
        startHomeDistance *= Random.Range(.9f, 1.1f);
        homeSpeed *= Random.Range(.9f, 1.2f);
        targetOffset = Random.onUnitSphere * targetPlanet.Radius;
    }



    void Update()
    {
        Move();
    }



    void Move()
    {
        if (target == null)
        {
            return;
        }

        // move to target
        direction = target.transform.position - transform.position;

        // no direction
        if (direction.sqrMagnitude <= 0)
        {
            return;
        }

        // move and rotate
        var directionNormalized = (direction + targetOffset).normalized;

        transform.position += transform.forward * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        if (distanceTraveled > startHomeDistance)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionNormalized, Vector3.up), 
                homeSpeed * Mathf.Clamp((Mathf.Pow(distanceTraveled, 2) / direction.sqrMagnitude), .5f, 30) * Time.deltaTime);
        }

        // detect hit
        Hit();
    }



    void Hit()
    {
        // hit target
        if (direction.sqrMagnitude <= Mathf.Pow(target.Radius, 2) * 1.05f)
        {
            if (target.Faction != faction && target.Faction != 0)
            {
                // damage
                target.Damage(damage);
                if (explosionFX != null)
                {
                    Instantiate(explosionFX, transform.position, Quaternion.LookRotation(-direction));
                }
            }
            else
            {
                // invade
                target.Invade(faction, damage);
            }
            Destroy(gameObject);
        }
    }



    public int GetFaction()
    {
        return faction;
    }
    #endregion
}
