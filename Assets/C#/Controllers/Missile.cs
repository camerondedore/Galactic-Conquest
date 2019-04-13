using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, ILaunch, IFaction
{
    #region Fields
    public Planet target;
    public float speed = 5,
        homeSpeed = 2;
    public int damage = 1;

    [SerializeField] GameObject explosionFX = null;

    Vector3 direction,
        targetOffset;
    float startHomeDistance = 0.5f,
        distanceTraveled = 0;
    int faction;
    #endregion

    #region Properties
    #endregion



    #region Methods
    public void Launch(Planet targetPlanet, int newFaction, int scale)
    {
        target = targetPlanet;
        faction = newFaction;

        speed *= Random.Range(.9f, 1.2f);
        startHomeDistance *= Random.Range(.9f, 1.1f);
        homeSpeed *= Random.Range(.9f, 1.2f);
        targetOffset = Random.onUnitSphere * targetPlanet.Radius;

        transform.localScale = transform.localScale * ( .25f * scale + .75f);

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

        if (direction.sqrMagnitude <= 0)
        {
            return;
        }

        transform.position += transform.forward * speed * Time.deltaTime;
        distanceTraveled += speed * Time.deltaTime;

        var directionNormalized = (direction + targetOffset).normalized;

        if (distanceTraveled > startHomeDistance && transform.forward != directionNormalized)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionNormalized, Vector3.up), 
                homeSpeed * Mathf.Clamp((Mathf.Pow(distanceTraveled, 2) / direction.sqrMagnitude), .5f, 30) * Time.deltaTime);
        }

        Hit();
    }



    void Hit()
    {
        if (direction.sqrMagnitude <= Mathf.Pow(target.Radius, 2) * 1.05f)
        {
            target.Damage(faction, damage);

            if (explosionFX != null && target.Faction != faction)
            {
                GameObject fx = Instantiate(explosionFX, transform.position, Quaternion.LookRotation(-direction)) as GameObject;
                fx.transform.localScale *= (.25f * damage + .75f);
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
