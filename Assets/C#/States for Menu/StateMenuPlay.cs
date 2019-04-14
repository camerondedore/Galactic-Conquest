using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMenuPlay : State
{

	public bool ClickedPlay { get; set; } = false;
	public bool ClickedBack {get; set;} = false;

	[SerializeField] GameObject playMenu = null;
	[SerializeField] GameObject canvas = null;



	void Start()
	{
		blackBoard["PlayState"] = this;
	}



	public override void RunState()
	{

	}



	public override void StartState()
	{
		playMenu.SetActive(true);
		ClickedBack = false;
		ClickedPlay = false;
	}



	public override void EndState()
	{
		playMenu.SetActive(false);
	}



	public override State Transition()
	{
		if (ClickedPlay)
		{
			StartCoroutine(Load());

			return (State)blackBoard["BaseState"];
		}

		if (ClickedBack)
		{
			return (State)blackBoard["BaseState"];
		}

		return this;
	}



	IEnumerator Load()
	{
		bool inGame = false;
		var i = 0;

		while (i < SceneManager.sceneCount)
		{
			if (SceneManager.GetSceneAt(i).name == "GameScene")
			{
				inGame = true;
			}
			i++;
		}

		AsyncOperation async;

		if (inGame)
		{
			async = SceneManager.UnloadSceneAsync("GameScene", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
			yield return async;
		}
		else
		{
			yield return null;
		}

		async = SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
		yield return async;
		SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
		canvas.SetActive(false);
	}
}
