using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionOverlay : MonoBehaviour
{
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
		DontDestroyOnLoad(overlay);

		Material mat = overlay.GetComponentInChildren<Image>().material;

		Debug.Log("Transitioning");

		mat.SetFloat("_Flip", 1);

		float fadeInTime = Singleton<GlobalData>.Instance.GlobalConfigInstance.PrimaryGameplayTuningValues.SceneTransitionFadeInTime;

		for (float i = 0; i < 1; i += Time.deltaTime / fadeInTime)
		{
			mat.SetFloat("_Fade", i);
			yield return null;
		}
		mat.SetFloat("_Fade", 1);

		Debug.Log("Transitioned, idling");

		yield return new WaitForSeconds(Singleton<GlobalData>.Instance.GlobalConfigInstance.PrimaryGameplayTuningValues.SceneTransitionIdleTime / 2f);

		Debug.Log("Loading");

		sceneLoadCallback(sceneName);

		Debug.Log("Loaded, idling");

		yield return new WaitForSeconds(Singleton<GlobalData>.Instance.GlobalConfigInstance.PrimaryGameplayTuningValues.SceneTransitionIdleTime / 2f);

		Debug.Log("Stopping transition");

		mat.SetFloat("_Flip", 0);

		float fadeOutTime = Singleton<GlobalData>.Instance.GlobalConfigInstance.PrimaryGameplayTuningValues.SceneTransitionFadeOutTime;

		for (float i = 0; i < 1; i += Time.deltaTime / fadeOutTime)
		{
			mat.SetFloat("_Fade", 1 - i);
			yield return null;
		}
		mat.SetFloat("_Fade", 0);

		Destroy(overlay);
	}
}
