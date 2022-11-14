using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthDataConfig", menuName = "Configs/Health Data Config")]
[System.Serializable]
public class HealthDataConfig : ScriptableObject
{
	[Tooltip("The maximum health this object can heal to")]
	public float MaxHealth = 100;

	[Tooltip("This object's starting health")]
	public float StartingHealth = 100;

	[Tooltip("The minimum amount of damage needed to actually receive impact damage")]
	public float ImpactDamageThreshold = 10;

	private void OnValidate() {
		MaxHealth = Mathf.Max(0, MaxHealth);
		StartingHealth = Mathf.Max(0, StartingHealth);
		ImpactDamageThreshold = Mathf.Max(0, ImpactDamageThreshold);
	}
}
