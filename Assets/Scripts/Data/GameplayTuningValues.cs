using UnityEngine;

[CreateAssetMenu(fileName = "NewGameplayTuningValues", menuName = "Gameplay Config/Gameplay Tuning Values")]
[System.Serializable]
public class GameplayTuningValues : ScriptableObject
{
	public float PlayerGunStrength = 80f;
	public float PlayerGunDetectionRadius = 5f;

	[Expandable] public PhysicsDataConfig PlayerPhysicsDataConfig;
}
