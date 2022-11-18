using UnityEngine;

[System.Serializable]
public enum ImpulseSourceTag
{
	Generic,
	Magnetic,
	PhysicalKnockback,
	DangerousKnockback,
	Explosive,
	Field,
}

[System.Serializable]
public struct ImpulseSourceType
{
	public static ImpulseSourceType defaultType = new ImpulseSourceType(ImpulseSourceTag.Generic);

	public ImpulseSourceTag impulseSourceTag;

	public ImpulseSourceType(ImpulseSourceTag istg)
	{
		impulseSourceTag = istg;
	}
}

public interface IImpulseReceiver
{
	public void ApplyImpulse(Vector3 direction, float strength);
	
	public void ApplyImpulse(Vector3 direction, float strength, ImpulseSourceType type);
}

