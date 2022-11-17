using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class DroneEnemy : MonoBehaviour
{
	//MagneticEntity get tethers (active tethers), if more than 0 start a timer.
	[SerializeField] float timeNeeded;
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
		player = FindObjectOfType<Player>().transform;
		aIDestinationSetter.target = player;
		aIPath.maxSpeed = pathfindingBehaviorConfig.MovementSpeed;
	}

	void Update() 
	{
		//Check every frame is a tether is still attached (if so keep falling)
		foreach (Anchor anchor in MagneticEntity.RetrieveActiveAnchors()) {
  			if (anchor.GetTethers().Count > 0) 
			{
    			// Do stuff
  			}
		}
	}

	void Falling()
	{

	}
}
