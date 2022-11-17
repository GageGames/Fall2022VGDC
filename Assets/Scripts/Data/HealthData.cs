using MyBox;
using UnityEngine;

// stores pure data on an object's current and max health

public class HealthData : MonoBehaviour
{
	[Expandable]
	[SerializeField] protected HealthDataConfig configData;

	[ReadOnly]
	public float CurrentHealth;
	[ReadOnly]
	public float MaxHealth;
	[ReadOnly]
	public float ImpactDamageThreshold;
	[ReadOnly]
	public bool Dead = false;

	void Awake()
	{
		if (!configData)
		{
			Debug.LogError("HealthData must have config data assigned!");
			CurrentHealth = 0;
			MaxHealth = 0;
			ImpactDamageThreshold = 0;
			return;
		}

		MaxHealth = configData.MaxHealth;
		CurrentHealth = configData.StartingHealth;
		ImpactDamageThreshold = configData.ImpactDamageThreshold;
	}
}
