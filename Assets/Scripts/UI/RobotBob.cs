using System;
using System.Collections.Generic;
using UnityEngine;

public class RobotBob : MonoBehaviour
{
	public float magnitude = 5;
	public float speed = 0.3f;
	private Vector3 origin;

	private void Start()
	{
		origin = transform.localPosition;
	}

	private void Update()
	{
		var offset = new Vector3(0, magnitude * Mathf.Sin(Time.time * speed), 0);
		transform.localPosition = origin + offset;
	}
}