using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

// A static, lazy-loaded sound effects manager
// Plays sound effects clips either on loop or instantaneously

public class SFXManager : MonoBehaviour
{
	// TODO: Genericize object pooling system
	//static Queue<GameObject> sourcePool = new Queue<GameObject>();

	public static void PlaySound(AudioClip clip, AudioMixerGroup mixerGroup, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(Vector3.zero, null);

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.outputAudioMixerGroup = mixerGroup;
		source.Play();

		Destroy(source.gameObject, clip.length);

		//Singleton<SFXManager>.Instance.StartCoroutine(RequeueSource(source, clip.length));
	}

	public static void PlaySound(AudioClip clip, AudioMixerGroup mixerGroup, Vector3 pos, Transform parent, float spatialBlend = 1, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(pos, parent);

		source.clip = clip;
		source.spatialBlend = spatialBlend;
		source.volume = volume;
		source.pitch = pitch;
		source.outputAudioMixerGroup = mixerGroup;
		source.Play();

		Destroy(source.gameObject, clip.length);

		//Singleton<SFXManager>.Instance.StartCoroutine(RequeueSource(source, clip.length));
	}

	public static void PlayLoopedSound(AudioClip clip, AudioMixerGroup mixerGroup, Func<bool> loopEndCondition, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(Vector3.zero, null);

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = true;
		source.outputAudioMixerGroup = mixerGroup;
		source.Play();

		Singleton<SFXManager>.Instance.StartCoroutine(LateDestroyLoopedSource(source, loopEndCondition));
	}

	public static void PlayLoopedSound(AudioClip clip, AudioMixerGroup mixerGroup, Func<bool> loopEndCondition, Vector3 pos, Transform parent, float spatialBlend = 1, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(pos, parent);

		source.clip = clip;
		source.spatialBlend = spatialBlend;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = true;
		source.outputAudioMixerGroup = mixerGroup;
		source.Play();

		Singleton<SFXManager>.Instance.StartCoroutine(LateDestroyLoopedSource(source, loopEndCondition));
	}
	/*
	static IEnumerator RequeueSource(AudioSource source, float clipLength)
	{
		yield return new WaitForSeconds(clipLength);
		sourcePool.Enqueue(source.gameObject);
	}*/

	static IEnumerator LateDestroyLoopedSource(AudioSource source, Func<bool> loopEndCondition)
	{
		while (!loopEndCondition())
		{
			yield return null;
		}

		Destroy(source.gameObject);

		//source.loop = false;
		//sourcePool.Enqueue(source.gameObject);
	}

	static AudioSource GetSource(Vector3 pos, Transform parent = null)
	{
		GameObject obj;

		/*if (sourcePool.Count > 0)
		{
			obj = sourcePool.Dequeue();
		}
		else
		{*/
		obj = new GameObject("SFX Source", components: typeof(AudioSource));
		//}

		obj.transform.position = pos;
		obj.transform.parent = parent == null ? Singleton<SFXManager>.Instance.transform : parent;

		return obj.GetComponent<AudioSource>();
	}
}
