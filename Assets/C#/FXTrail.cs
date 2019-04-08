using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXTrail : MonoBehaviour
{
    #region Fields
    Transform leader;
    #endregion

    #region Properties
    #endregion



    #region Methods
    void Start()
    {
        StartCoroutine(Delay());
    }



    private void Update()
    {
        if(leader != null)
        {
            transform.position = leader.position;
        }
    }



    IEnumerator Delay()
    {
        yield return null;
        leader = transform.parent;
        transform.parent = null;
    }
    #endregion
}
