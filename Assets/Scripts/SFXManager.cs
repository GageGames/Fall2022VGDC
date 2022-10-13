using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    public static SFXManager Instance;
    public static Transform ClipHolder;

    // TODO: Genericize object pooling system
    public static Queue<AudioSource> sourcePool = new Queue<AudioSource> ();

    // TODO: Manage singletons from a more central source?
    private void Awake () 
    {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad (gameObject);
        } else {
            Destroy (gameObject);
        }
    }

    public static void PlaySound (AudioClip clip) 
    {
        AudioSource source = GetSource ();

        source.clip = clip;
        source.Play ();

        Instance.StartCoroutine (RequeueSource (source, clip.length));
    }

    public static void PlaySound (AudioClip clip, float volume, float pitch) 
    {
        AudioSource source = GetSource ();

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play ();
        
        Instance.StartCoroutine (RequeueSource (source, clip.length));
    }

    public static void PlayLoopedSound (AudioClip clip, float volume, float pitch, Func<bool> loopEndCondition) 
    {
        AudioSource source = GetSource();

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = true;
        source.Play ();
            
        Instance.StartCoroutine (RequeueLoopedSource (source, loopEndCondition));
    }
    
    public static IEnumerator RequeueSource (AudioSource source, float clipLength) 
    {
        yield return new WaitForSeconds (clipLength);
        sourcePool.Enqueue (source);
    }

    public static IEnumerator RequeueLoopedSource (AudioSource source, Func<bool> loopEndCondition) 
    {
        while (!loopEndCondition()) 
        {
            yield return null;
        }

        source.loop = false;
        sourcePool.Enqueue (source);
    }

    static AudioSource GetSource () {
        if (sourcePool.Count > 0) 
        {
            return sourcePool.Dequeue ();
        } else 
        {
            return new GameObject ("Audio Source", components: typeof (AudioSource)).GetComponent<AudioSource> ();
        }
    }
}
