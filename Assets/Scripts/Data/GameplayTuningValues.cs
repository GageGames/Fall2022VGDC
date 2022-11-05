using UnityEngine;

[CreateAssetMenu(fileName = "NewGameplayTuningValues", menuName = "Configs/Gameplay Tuning Values")]
[System.Serializable]
public class GameplayTuningValues : ScriptableObject
{
	public float PlayerGunPullStrength = 80f;
	public float PlayerGunPushStrength = 50f;
	public float PlayerGunDetectionRadius = 5f;

	[Expandable] public PhysicsDataConfig PlayerPhysicsDataConfig;
}
