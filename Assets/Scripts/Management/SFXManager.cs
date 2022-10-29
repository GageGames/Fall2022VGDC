﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A static, lazy-loaded sound effects manager
// Plays sound effects clips either on loop or instantaneously

public class SFXManager : MonoBehaviour
{
	// TODO: Genericize object pooling system
	public static Queue<GameObject> sourcePool = new Queue<GameObject>();

	private void Awake()
	{
		if (Singleton<SFXManager>.Instance != null && Singleton<SFXManager>.Instance != this)
		{
			Destroy(gameObject);
		}
	}

	public static void PlaySound(AudioClip clip, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(Vector3.zero, null);

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.Play();

		Singleton<SFXManager>.Instance.StartCoroutine(RequeueSource(source, clip.length));
	}

	public static void PlaySound(AudioClip clip, Vector3 pos, Transform attachedTo, float spatialBlend = 1, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(pos, attachedTo);

		source.clip = clip;
		source.spatialBlend = spatialBlend;
		source.volume = volume;
		source.pitch = pitch;
		source.Play();

		Singleton<SFXManager>.Instance.StartCoroutine(RequeueSource(source, clip.length));
	}

	public static void PlayLoopedSound(AudioClip clip, Func<bool> loopEndCondition, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(Vector3.zero, null);

		source.clip = clip;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = true;
		source.Play();

		Singleton<SFXManager>.Instance.StartCoroutine(RequeueLoopedSource(source, loopEndCondition));
	}

	public static void PlayLoopedSound(AudioClip clip, Func<bool> loopEndCondition, Vector3 pos, Transform attachedTo, float spatialBlend = 1, float volume = 1, float pitch = 1)
	{
		AudioSource source = GetSource(pos, attachedTo);

		source.clip = clip;
		source.spatialBlend = spatialBlend;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = true;
		source.Play();

		Singleton<SFXManager>.Instance.StartCoroutine(RequeueLoopedSource(source, loopEndCondition));
	}

	public static IEnumerator RequeueSource(AudioSource source, float clipLength)
	{
		yield return new WaitForSeconds(clipLength);
		sourcePool.Enqueue(source.gameObject);
	}

	public static IEnumerator RequeueLoopedSource(AudioSource source, Func<bool> loopEndCondition)
	{
		while (!loopEndCondition())
		{
			yield return null;
		}

		source.loop = false;
		sourcePool.Enqueue(source.gameObject);
	}

	static AudioSource GetSource(Vector3 pos, Transform attachedTo = null)
	{
		GameObject obj;

		if (sourcePool.Count > 0)
		{
			obj = sourcePool.Dequeue();
		}
		else
		{
			obj = new GameObject("SFX Source", components: typeof(AudioSource));
		}

		obj.transform.position = pos;
		obj.transform.parent = attachedTo == null ? Singleton<SFXManager>.Instance.transform : attachedTo;

		return obj.GetComponent<AudioSource>();
	}
}
