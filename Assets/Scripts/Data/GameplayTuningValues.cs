using UnityEngine;

[CreateAssetMenu(fileName = "NewGameplayTuningValues", menuName = "Configs/Gameplay Tuning Values")]
[System.Serializable]
public class GameplayTuningValues : ScriptableObject
{
	[Header("Player")]

	public float PlayerGunPullStrength = 80f;
	public float PlayerGunPushStrength = 50f;
	public float PlayerGunDetectionRadius = 5f;
	public LayerMask PlayerGunDetectionMask;

	[Space]

	[Expandable] public PhysicsDataConfig PlayerPhysicsDataConfig;
	[Expandable] public HealthDataConfig PlayerHealthDataConfig;

	[Space]
	[Header("Zombie Enemy")]

	[Expandable] public PhysicsDataConfig ZombiePhysicsDataConfig;
	[Expandable] public HealthDataConfig ZombieHealthDataConfig;

	[Space]

	[Expandable] public ContactImpactorConfig ZombieImpactorConfig;
	[Expandable] public PathfindingBehaviorConfig ZombiePathfindingBehaviorConfig;

	[Space]
	[Header("Special Objects")]

	[Expandable] public HealthDataConfig BarrelHealthDataConfig;
	[Expandable] public ExplosiveConfig BarrelExplosiveConfig;

	[Space]

	[Expandable] public PitConfig PitConfig;

	[Space]
	[Header("Props")]

	[Expandable] public PhysicsDataConfig LightPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig LightPropHealthDataConfig;
	[Expandable] public PhysicsDataConfig MediumPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig MediumPropHealthDataConfig;
	[Expandable] public PhysicsDataConfig HeavyPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig HeavyPropHealthDataConfig;

	[Space]

	[Expandable] public PhysicsDataConfig StaticPropPhysicsDataConfig;
	[Expandable] public HealthDataConfig StaticPropHealthDataConfig;

	[Space]

	[Expandable] public ContactImpactorConfig HazardPropImpactorConfig;
}
