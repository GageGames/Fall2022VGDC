using UnityEngine;

[CreateAssetMenu(fileName = "NewGameplayTuningValues", menuName = "Configs/Gameplay Tuning Values")]
[System.Serializable]
public class GameplayTuningValues : ScriptableObject
{
	[Header("Player")]

	public float PlayerGunPullStrength = 80f;
	public float PlayerGunPushStrength = 50f;
	public float PlayerGunDetectionRadius = 5f;

	[Expandable] public PhysicsDataConfig PlayerPhysicsDataConfig;
	[Expandable] public HealthDataConfig PlayerHealthDataConfig;

	[Header("Zombie Enemy")]

	[Expandable] public PhysicsDataConfig ZombiePhysicsDataConfig;
	[Expandable] public HealthDataConfig ZombieHealthDataConfig;

	[Expandable] public ContactImpactorConfig ZombieImpactorConfig;

	[Header("Props")]

	[Expandable] public PhysicsDataConfig LightPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig LightPropHealthDataConfig;
	[Expandable] public PhysicsDataConfig MediumPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig MediumPropHealthDataConfig;
	[Expandable] public PhysicsDataConfig HeavyPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig HeavyPropHealthDataConfig;

	[Expandable] public PhysicsDataConfig StaticPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig StaticPropHealthDataConfig;

	[Expandable] public ContactImpactorConfig HazardPropImpactorConfig;
}
