using UnityEngine;
using UnityEngine.Events;

public struct HealthEffectSourceType
{
	public static HealthEffectSourceType defaultType = new HealthEffectSourceType(HealthEffectSourceTag.Generic);

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
	public float ApplyDamage(float amount)
	{
		return ApplyDamage(amount, HealthEffectSourceType.defaultType);
	}

	public float ApplyHeal(float amount)
	{
		return ApplyHeal(amount, HealthEffectSourceType.defaultType);
	}

	public float ApplyDamage(float amount, HealthEffectSourceType damageSourceType)
	{
		data.CurrentHealth = Mathf.Max(0, data.CurrentHealth - amount);

		OnDamage.Invoke();
		if (data.CurrentHealth == 0) {
			OnDeath.Invoke();
		}

		return data.CurrentHealth;
	}

	public float ApplyHeal(float amount, HealthEffectSourceType healingSourceType)
	{
		data.CurrentHealth = Mathf.Min(data.MaxHealth, data.CurrentHealth + amount);

		OnHeal.Invoke();

		return data.CurrentHealth;
	}
}
