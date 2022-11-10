using UnityEngine;

[CreateAssetMenu(fileName = "NewImpactorDataConfig", menuName = "Configs/Impactor Data Config")]
[System.Serializable]
public class ImpactorConfig : ScriptableObject
{
	[Tooltip("The amount of damage to automatically deal on contact with another object")]
	public float ConstantContactDamage = 0;

	[Tooltip("The amount of force to automatically apply on contact with another object")]
	public float ConstantContactKnockback = 0;
}
