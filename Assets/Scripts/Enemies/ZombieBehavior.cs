using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIDestinationSetter))]
[RequireComponent(typeof(AIPath))]
public class ZombieBehavior : MonoBehaviour
{
	[Expandable]
	public PathfindingBehaviorConfig pathfindingBehaviorConfig;

	Transform player;
	AIDestinationSetter aIDestinationSetter;
	AIPath aIPath;

	private void Awake()
	{
		aIDestinationSetter = GetComponent<AIDestinationSetter>();
		aIPath = GetComponent<AIPath>();
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
	}
}
