using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UIPlanetCounter : MonoBehaviour
{
	[SerializeField] GameObject planetCounter = null;
	List<TextMeshProUGUI> planetCounterLabels = new List<TextMeshProUGUI>();
	List<int> planetCounts = new List<int>();
	int factionCount = -1;



	void Start()
	{
		var uniqueFactionPlanets = Planet.Planets.GroupBy(p => p.Faction).Select(g => g.First()).OrderBy(p => p.Faction).ToList();
		factionCount = uniqueFactionPlanets.Count;

		foreach (Planet p in uniqueFactionPlanets)
		{
			var counter = Instantiate(planetCounter, transform.position, transform.rotation, transform);
			var tmLabel = counter.GetComponent<TextMeshProUGUI>();
			tmLabel.color = FXFactionColor.factionColors[p.Faction];
			planetCounterLabels.Add(tmLabel);
		}
	}



	void Update()
	{
		planetCounts = new List<int>();

		int i = 0;
		while (i <= factionCount)
		{
			planetCounts.Add(0);
			i++;
		}

		foreach (Planet p in Planet.Planets)
		{
			planetCounts[p.Faction]++;
		}

		i = 0;
		foreach (TextMeshProUGUI l in planetCounterLabels)
		{
			l.text = planetCounts[i].ToString();
			i++;
		}
	}
}
