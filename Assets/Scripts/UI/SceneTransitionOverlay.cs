using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionOverlay : MonoBehaviour
{
	public float FadeInTime = 4;
	public float SceneLoadIdleTime = 2;
	public float FadeOutTime = 4;

	public void BeginTransition(Action<string> ContinueCallback, string sceneName)
	{
		StartCoroutine(Transition(ContinueCallback, sceneName));
	}

	public void EndTransition()
	{
		StartCoroutine(StopTransition());
	}

	// hehe hrt go brrrr
	IEnumerator Transition(Action<string> ContinueCallback, string sceneName)
	{
		GameObject overlay = Instantiate(Singleton<GlobalData>.Instance.GlobalConfigInstance.SceneTransitionOverlayPrefab);

		Material mat = overlay.GetComponentInChildren<Image>().material;

		for (float i = 0; i < 1; i += Time.deltaTime / FadeInTime)
		{
			mat.SetFloat("Fade Progress", FadeInTime);
			yield return null;
		}
		
		Debug.Log("Transitioned, idling");

		yield return new WaitForSeconds(SceneLoadIdleTime);

		Debug.Log("Loading");

		ContinueCallback(sceneName);
	}

	// no I'm not naming this detransition fuck you
	IEnumerator StopTransition()
	{
		GameObject overlay = Instantiate(Singleton<GlobalData>.Instance.GlobalConfigInstance.SceneTransitionOverlayPrefab);

		Material mat = overlay.GetComponentInChildren<Image>().material;

		Debug.Log("Loaded, idling");

		yield return new WaitForSeconds(SceneLoadIdleTime);

		Debug.Log("Stopping transition");

		for (float i = 0; i < 1; i += Time.deltaTime / FadeInTime)
		{
			mat.SetFloat("Fade Progress", 1 - FadeInTime);
			yield return null;
		}
	}
}
