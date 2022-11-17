using Pathfinding;
using UnityEngine;

/// <summary>
/// Required components:
/// AIDestinationSetter
/// AIPath
/// 
/// </summary>

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
		aIDestinationSetter.target = player;
		aIPath.maxSpeed = pathfindingBehaviorConfig.MovementSpeed;
	}
}
