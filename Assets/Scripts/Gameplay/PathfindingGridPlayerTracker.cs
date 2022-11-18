using Pathfinding;
using UnityEngine;

public class PathfindingGridPlayerTracker : MonoBehaviour
{
	void Start()
	{
		Transform player = FindObjectOfType<Player>()?.transform;
		if (!player)
		{
			Debug.LogError("Failed to track player for pathfinding");
			return;
		}
		GetComponent<ProceduralGridMover>().target = player;
		player.GetComponent<HealthEntity>().OnDeath.AddListener(StopTracking);
	}

	void StopTracking(HealthEntity healthEntity)
	{
		GetComponent<ProceduralGridMover>().target = transform;
	}
}
