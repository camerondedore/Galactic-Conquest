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
    public void Launch(Planet targetPlanet, int newFaction, int scale)
    {
        target = targetPlanet;
        faction = newFaction;

        // randomize
        speed *= Random.Range(.9f, 1.2f);
        startHomeDistance *= Random.Range(.9f, 1.1f);
        homeSpeed *= Random.Range(.9f, 1.2f);
        targetOffset = Random.onUnitSphere * targetPlanet.Radius;

        // scale
        transform.localScale = transform.localScale * scale * 0.5f;

        // adjust damage
        damage *= scale;
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

        direction = target.transform.position - transform.position;

        // no direction
        if (direction.sqrMagnitude <= 0)
        {
            return;
        }

        // move to target
        transform.position += transform.forward * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        // rotate
        var directionNormalized = (direction + targetOffset).normalized;

        if (distanceTraveled > startHomeDistance && transform.forward != directionNormalized)
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
                    GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.LookRotation(-direction)) as GameObject;

                    // scale fx
                    fx.transform.localScale *= damage;
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
