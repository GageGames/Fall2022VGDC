using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionOverlay : MonoBehaviour
{
	public float FadeInTime = 4;
	public float SceneLoadIdleTime = 2;
	public float FadeOutTime = 4;

	private void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void BeginTransition(Action<string> sceneLoadCallback, string sceneName)
	{
		StartCoroutine(Transition(sceneLoadCallback, sceneName));
	}

	// hehe hrt go brrrr
	IEnumerator Transition(Action<string> sceneLoadCallback, string sceneName)
	{
		GameObject overlay = Instantiate(Singleton<GlobalData>.Instance.GlobalConfigInstance.SceneTransitionOverlayPrefab);

		Material mat = overlay.GetComponentInChildren<Image>().material;

		for (float i = 0; i < 1; i += Time.deltaTime / FadeInTime)
		{
			mat.SetFloat("Fade Progress", FadeInTime);
			yield return null;
		}
		mat.SetFloat("Fade Progress", 1);

		Debug.Log("Transitioned, idling");

		yield return new WaitForSeconds(SceneLoadIdleTime);

		Debug.Log("Loading");

		sceneLoadCallback(sceneName);

		Debug.Log("Loaded, idling");

		yield return new WaitForSeconds(SceneLoadIdleTime);

		Debug.Log("Stopping transition");

		for (float i = 0; i < 1; i += Time.deltaTime / FadeInTime)
		{
			mat.SetFloat("Fade Progress", 1 - FadeInTime);
			yield return null;
		}
		mat.SetFloat("Fade Progress", 0);

		Destroy(overlay);
	}
}
