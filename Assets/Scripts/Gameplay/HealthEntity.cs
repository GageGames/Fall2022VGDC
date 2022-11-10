using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// handles changing an object's health

[RequireComponent(typeof(HealthData))]
public class HealthEntity : MonoBehaviour
{
	private HealthData data;

	public UnityEvent OnDamage = new UnityEvent();
	public UnityEvent OnHeal = new UnityEvent();
	public UnityEvent OnDeath = new UnityEvent();

	void Awake() {
		data = GetComponent<HealthData>();
	}

	// ***both functions return new CurrentHealth, after operation
	public float ApplyDamage(float amount) {
		data.CurrentHealth = Mathf.Max(0, data.CurrentHealth - amount);

		OnDamage.Invoke();
		if (data.CurrentHealth == 0) {
			OnDeath.Invoke();
		}

		return data.CurrentHealth;
	}

	public float ApplyHeal(float amount) {
		data.CurrentHealth = Mathf.Min(data.MaxHealth, data.CurrentHealth + amount);

		OnHeal.Invoke();

		return data.CurrentHealth;
	}
}
