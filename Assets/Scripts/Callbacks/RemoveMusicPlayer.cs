using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RemoveMusicPlayer : MonoBehaviour
{
	public float duration = 2f;
	
	public void Remove()
	{
		MusicPlayer player = FindObjectOfType<MusicPlayer>();
		player.GetComponent<AudioSource>().DOFade(0, duration);
		Destroy(player.gameObject, duration);
	}
}