using UnityEngine;

[CreateAssetMenu(fileName = "NewExplosiveConfig", menuName = "Configs/Explosive Config")]
[System.Serializable]

public class ExplosiveConfig : ScriptableObject
{
	[Tooltip("The size of the radius within which entities are affected by this explosion")]
	public float ExplosionRadius = 5f;

	[Tooltip("The maximum physics force the explosion can apply")]
	public float ExplosionStrength = 5f;

	[Tooltip("The maximum damage the explosion can apply")]
	public float ExplosionDamage = 5f;

	[Tooltip("The falloff curve of both damage and physics force strength based on the distance of the target object from the explosion epicenter. " +
		"Ranges from 0 to 1, where 1 is interpreted as distance equal to ExplosionRadius")]
	public AnimationCurve ExplosionFalloff;
}
