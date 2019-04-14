using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialEnd : MonoBehaviour
{

	[SerializeField] RawImage fadeImage = null;



	void Start()
	{
		StartCoroutine(FadeIn());
	}



	private void OnMouseUp()
	{
		StartCoroutine(FadeOut());
	}



	IEnumerator FadeOut()
	{
		var color = fadeImage.color;
		while (fadeImage.color.a < 1)
		{
			color.a += Time.deltaTime;
			fadeImage.color = color;
			yield return null;
		}

		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
	}



	IEnumerator FadeIn()
	{
		var color = fadeImage.color;
		while (fadeImage.color.a > 0)
		{
			color.a -= Time.deltaTime;
			fadeImage.color = color;
			yield return null;
		}
	}
}
