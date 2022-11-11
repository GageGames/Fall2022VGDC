using UnityEngine;

// stores pure data on an object's current and max health

public class HealthData : MonoBehaviour
{
	[Expandable]
	[SerializeField] protected HealthDataConfig configData;

	[HideInInspector]
	public float CurrentHealth;
	[HideInInspector]
	public float MaxHealth;
	[HideInInspector]
	public float ImpactDamageThreshold;
	[HideInInspector]
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
