using UnityEngine;

[CreateAssetMenu(fileName = "NewPathfindingBehaviorConfig", menuName = "Configs/Pathfinding Behavior Config")]
[System.Serializable]
public class PathfindingBehaviorConfig : ScriptableObject
{
	[Tooltip("The maximum speed this pathfinding entity can move at")]
	[Min(0f)]
	public float MovementSpeed;
}
