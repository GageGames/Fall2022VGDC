using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]

// MagneticEntity get tethers (active tethers), if more than 0 start a timer.

public class DroneEnemy : MonoBehaviour
{
	[SerializeField] float timeNeeded;
	[SerializeField] GameObject droneVisualObject;

	[Expandable]
	public PathfindingBehaviorConfig pathfindingBehaviorConfig;

	float timer;

	Transform player;
	MagneticEntity magneticEntity;
	HealthEntity healthEntity;
	AIDestinationSetter aIDestinationSetter;
	AIPath aIPath;

	private void Awake()
	{
		aIDestinationSetter = GetComponent<AIDestinationSetter>();
		aIPath = GetComponent<AIPath>();
		magneticEntity = GetComponent<MagneticEntity>();
		healthEntity = GetComponent<HealthEntity>();
	}

	void Start()
	{
		player = FindObjectOfType<Player>()?.transform;

		if (!player)
		{
			Debug.LogError("Failed to assign player!");
			return;
		}

		aIDestinationSetter.target = player;
		aIPath.maxSpeed = pathfindingBehaviorConfig.MovementSpeed;

		timer = timeNeeded;
	}

	void Update()
	{
		//Check every frame is a tether is still attached (if so keep falling)
		foreach (Anchor anchor in magneticEntity.RetrieveActiveAnchors())
		{
			if (anchor.GetTethers().Count > 0)
			{
				timer -= Time.deltaTime;
				//Debug.Log(timer);
				if (timer <= 0f)
				{
					//Debug.Log("Falling!");
					Falling();
				}
			}
			else
			{
				timer = timeNeeded;
			}
		}

	}

	void Falling()
	{
		healthEntity.ApplyDamage(999);
	}
}
