using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Gun))]
public class TestPlayer : MonoBehaviour
{

	[SerializeField]
	Gun gun;


	// TODO: State machine this :P
	bool pulling = false;
	bool pushing = false;

	void Update()
    {
		if (Input.GetMouseButtonDown(0) && !pulling && !pushing)
		{
			// TODO: REFACTOR
			// Replace with InputHandler.GetMouseWorldPos
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				gun.Fire(hit.point, true);
			}

			pulling = true;
		}

		else if (Input.GetMouseButtonDown(1) && !pushing && !pulling)
		{
			// TODO: REFACTOR
			// Replace with InputHandler.GetMouseWorldPos
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				gun.Fire(hit.point, false);
			}

			pushing = true;
		}

		if (Input.GetMouseButtonUp(0) && pulling)
		{
			gun.Detach();
			pulling = false;
		}

		if (Input.GetMouseButtonUp(1) && pushing)
		{
			gun.Detach();
			pushing = false;
		}
	}
}
