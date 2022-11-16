using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{

	public GameObject levelCompleteScreen;

	//for some reason collider doesn't recongize "Player" tag 
	public GameObject player;

	private void Start()
	{
		player = GameObject.Find("Cube Player");
	}

	private void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == player)
		{
			levelCompleteScreen.SetActive(true);
			Debug.Log("Trigger");
		}
	}



}
