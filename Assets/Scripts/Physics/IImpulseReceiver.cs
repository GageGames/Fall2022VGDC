using UnityEngine;

public enum ImpulseSourceTag
{
	Generic = 0,
	Magnetic = 1,
	PhysicalKnockback = 2,
	DangerousKnockback = 3,
	Explosive = 4,
}

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

