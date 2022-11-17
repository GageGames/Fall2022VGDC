using UnityEngine;
using UnityEngine.Events;

public struct HealthEffectSourceType
{
	public static HealthEffectSourceType DefaultType = new HealthEffectSourceType(HealthEffectSourceTag.Generic);

	public HealthEffectSourceTag damageSourceTag;

	public HealthEffectSourceType(HealthEffectSourceTag dst)
	{
		damageSourceTag = dst;
	}
}

public enum HealthEffectSourceTag
{
	Generic,
	Explosive,
	Knockback,
	Impact,
	Pit,
	AreaOfEffect,
	Pickup
}

// Handles changing an object's health

[RequireComponent(typeof(HealthData))]
public class HealthEntity : MonoBehaviour
{
	private HealthData data;

	[HideInInspector]
	public UnityEvent OnDamage = new UnityEvent();
	[HideInInspector]
	public UnityEvent OnHeal = new UnityEvent();
	[HideInInspector]
	public UnityEvent<HealthEntity> OnDeath = new UnityEvent<HealthEntity>();	

	void Awake() {
		data = GetComponent<HealthData>();
	}

	// ***both functions return new CurrentHealth, after operation
	public float ApplyDamage(float amount)
	{
		return ApplyDamage(amount, HealthEffectSourceType.DefaultType);
	}

	public float ApplyHeal(float amount)
	{
		return ApplyHeal(amount, HealthEffectSourceType.DefaultType);
	}

	public float ApplyDamage(float amount, HealthEffectSourceType damageSourceType)
	{
		//Debug.Log($"Received {amount} damage of type {damageSourceType.damageSourceTag}");

		// Don't take damage after death
		if (data.Dead) return 0;

		// Ignore impact damage if it does not reach the minimum threshold
		if (damageSourceType.damageSourceTag == HealthEffectSourceTag.Impact && amount < data.ImpactDamageThreshold)
		{
			//Debug.Log("Damage below threshold");
			return 0;
		}

		data.CurrentHealth = Mathf.Max(0, data.CurrentHealth - amount);

		OnDamage.Invoke();

		if (data.CurrentHealth < Mathf.Epsilon) {
			Die();
		}

		return data.CurrentHealth;
	}

	public float ApplyHeal(float amount, HealthEffectSourceType healingSourceType)
	{
		// Don't take healing after death
		if (data.Dead) return 0;

		data.CurrentHealth = Mathf.Min(data.MaxHealth, data.CurrentHealth + amount);

		OnHeal.Invoke();

		return data.CurrentHealth;
	}

	void Die ()
	{
		data.Dead = true;
		OnDeath.Invoke(this);
		Destroy(gameObject);
	}
}
