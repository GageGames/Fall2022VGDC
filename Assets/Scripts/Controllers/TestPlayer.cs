using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class TestPlayer : MonoBehaviour
{

	[SerializeField]
	Gun gun;

	void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			// TODO: REFACTOR
			// Replace with InputHandler.GetMouseWorldPos
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				gun.Fire(hit.point);
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			gun.Detach();
		}
	}
}
