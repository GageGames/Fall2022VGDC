using UnityEngine;

[CreateAssetMenu(fileName = "NewContactImpactorDataConfig", menuName = "Configs/Contact Impactor Data Config")]
[System.Serializable]
public class ContactImpactorConfig : ScriptableObject
{
	[Tooltip("The amount of damage to deal on contact with another object")]
	public float Damage = 0;

	[Tooltip("The amount of force to apply on contact with another object")]
	public float Knockback = 0;

	[Tooltip("The type of damage this contact would be tagged as")]
	public HealthEffectSourceType contactDamageSourceType = new HealthEffectSourceType(HealthEffectSourceTag.Impact);

	[Tooltip("The type of physics force this contact would be tagged as")]
	public ImpulseSourceType contactImpulseSourceType = new ImpulseSourceType(ImpulseSourceTag.PhysicalKnockback);
}
