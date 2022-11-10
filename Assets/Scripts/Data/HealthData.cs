using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// stores pure data on an object's current and max health

public class HealthData : MonoBehaviour
{
	[SerializeField] protected HealthDataConfig configData;
	
	public float CurrentHealth;
	public float MaxHealth;

	void Awake() {
		if (!configData)
		{
			Debug.LogError("HealthData must have config data assigned!");
			CurrentHealth = 0;
			MaxHealth = 0;
			return;
		}

		MaxHealth = configData.MaxHealth;
		CurrentHealth = configData.StartingHealth;
	}
}
