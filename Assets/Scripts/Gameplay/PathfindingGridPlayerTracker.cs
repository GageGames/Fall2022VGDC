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

	}
}
