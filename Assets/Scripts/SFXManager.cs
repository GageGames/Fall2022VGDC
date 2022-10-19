using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A static, lazy-loaded sound effects manager
// Plays sound effects clips either on loop or instantaneously

public class SFXManager : MonoBehaviour
{

	public static SFXManager Instance
	{
		get
		{
			if (!instance)
			{
				Init();
			}
			return instance;
		}
		private set
		{
			instance = value;
		}
	}

	// TODO: Genericize object pooling system
	public static Queue<AudioSource> sourcePool = new Queue<AudioSource>();


	static SFXManager instance = null;

	// TODO: Manage singletons from a more central source?
	private void Awake()
	{
		if (instance && instance != this)
		{
			Destroy(gameObject);
		}
	}

	static void Init()
	{
		instance = new GameObject("SFX Manager").AddComponent<SFXManager>();
		DontDestroyOnLoad(instance.gameObject);
	}

	public static void PlaySound(AudioClip clip)
	{
		AudioSource source = GetSource();

		source.clip = clip;
		source.Play();

		Instance.StartCoroutine(RequeueSource(source, clip.length));
	}

	public static void PlaySound(AudioClip clip, float volume, float pitch)
	{
		AudioSource source = GetSource();

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play();

		Instance.StartCoroutine(RequeueSource(source, clip.length));
	}

	public static void PlayLoopedSound(AudioClip clip, Func<bool> loopEndCondition)
	{
		AudioSource source = GetSource();

		source.clip = clip;
		source.loop = true;
		source.Play();

		Instance.StartCoroutine(RequeueLoopedSource(source, loopEndCondition));
	}

	public static void PlayLoopedSound(AudioClip clip, float volume, float pitch, Func<bool> loopEndCondition)
	{
		AudioSource source = GetSource();

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = true;
		source.Play();

		Instance.StartCoroutine(RequeueLoopedSource(source, loopEndCondition));
	}

	public static IEnumerator RequeueSource(AudioSource source, float clipLength)
	{
		yield return new WaitForSeconds(clipLength);
		sourcePool.Enqueue(source);
	}

	public static IEnumerator RequeueLoopedSource(AudioSource source, Func<bool> loopEndCondition)
	{
		while (!loopEndCondition())
		{
			yield return null;
		}

		source.loop = false;
		sourcePool.Enqueue(source);
	}

	static AudioSource GetSource()
	{
		if (sourcePool.Count > 0)
		{
			return sourcePool.Dequeue();
		}
		else
		{
			GameObject obj = new GameObject("SFX Source", components: typeof(AudioSource));
			obj.transform.parent = Instance.transform;
			return obj.GetComponent<AudioSource>();
		}
	}
}
