using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMenuBase : State
{

	public bool ClickedPlay { get; set; } = false;
	public bool ClickedQuit { get; set; } = false;

	[SerializeField] GameObject baseMenu = null;



	void Start()
	{
		blackBoard["BaseState"] = this;
	}



	public override void RunState()
	{

	}



	public override void StartState()
	{
		baseMenu.SetActive(true);
		ClickedPlay = false;
		ClickedQuit = false;
	}



	public override void EndState()
	{
		baseMenu.SetActive(false);
	}



	public override State Transition()
	{
		if (ClickedPlay)
		{
			return (State)blackBoard["PlayState"];
		}

		if (ClickedQuit)
		{
			Application.Quit();
		}


		return this;
	}
}
