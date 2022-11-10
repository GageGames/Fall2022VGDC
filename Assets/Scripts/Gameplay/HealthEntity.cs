using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// handles changing an object's health

[RequireComponent(typeof(HealthData))]
public class HealthEntity : MonoBehaviour
{
	private HealthData data;

	void Awake() {
		data = GetComponent<HealthData>();
	}

	// ***both functions return new CurrentHealth, after operation
	public float ApplyDamage(float amount) {
		data.CurrentHealth = Mathf.Max(0, data.CurrentHealth - amount);
		return data.CurrentHealth;
	}

	public float ApplyHeal(float amount) {
		data.CurrentHealth = Mathf.Min(data.MaxHealth, data.CurrentHealth + amount);
		return data.CurrentHealth;
	}
}
