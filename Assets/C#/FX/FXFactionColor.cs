using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public interface IFaction
{
    int GetFaction();
}




public class FXFactionColor : MonoBehaviour
{
    #region Fields
    public static Color[] factionColors = { new Color(.5f, .5f, .5f), new Color(.1f, .1f, 1f), new Color(1f, .1f, .1f), new Color(1, 1, .1f), new Color(.1f, 1, 1) };

	[SerializeField] float bloom = 1;
	#endregion

    #region Properties
    #endregion



    #region Methods
    void Awake()
    {
        StartCoroutine(Delay());
    }



    IEnumerator Delay()
    {
        yield return null;

        var faction = transform.root.GetComponent<IFaction>().GetFaction();
		var newColor = factionColors[faction];
		newColor.a = bloom;
		GetComponent<Renderer>().material.SetColor("_Color", newColor);
        GetComponent<Renderer>().material.SetColor("_EmissionColor", newColor);
    }
    #endregion
}
