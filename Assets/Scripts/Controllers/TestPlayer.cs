using UnityEngine;

[RequireComponent(typeof(Gun))]
public class TestPlayer : MonoBehaviour
{
	Gun gun;

	// TODO: State machine this :P
	bool pulling = false;
	bool pushing = false;

	[SerializeField]
	private GameplayTuningValues val;

	private void Awake()
	{
		gun = GetComponent<Gun>();

		gun.Strength = val.PlayerGunPullStrength;
		gun.DetectionRadius = val.PlayerGunDetectionRadius;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0) && !pulling && !pushing)
		{
			gun.Strength = val.PlayerGunPullStrength;

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
			gun.Strength = val.PlayerGunPushStrength;

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
